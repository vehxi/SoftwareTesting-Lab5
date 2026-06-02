#!/usr/bin/env python3
# generator/gen_tests.py
"""
Генератор параметризованных NUnit-тестов на основе YAML-спецификации.
Читает формальное описание, преобразует классы эквивалентности в [TestCase],
генерирует структуру Arrange-Act-Assert и сохраняет файл в указанный каталог.
Зависимости: pyyaml (pip install pyyaml)
"""
import yaml
import argparse
import os
from pathlib import Path
from typing import Dict, List, Any

# ---------------------------------------------------------
# 1. ШАБЛОНЫ КОДА
# ---------------------------------------------------------
TEST_FILE_TEMPLATE = """// =============================================================
// AUTO-GENERATED TESTS. DO NOT EDIT MANUALLY.
// Source: {spec_source}
// Generator: gen_tests.py v1.0
// =============================================================
using System;
using System.Collections.Generic;
using NUnit.Framework;
using Lab.Interfaces;
using Lab.Implementations;

namespace Module.Tests
{{
    [TestFixture]
    [Description("Автоматически сгенерированные тесты для {module_name}")]
    public class {module_name}Tests
    {{
        private I{module_name} _sut;

        [SetUp]
        public void SetUp()
        {{
            // Инициализация тестируемой системы (SUT)
            _sut = new {module_name}();
        }}

{test_methods}
    }}
}}
"""

TEST_METHOD_TEMPLATE = """
        [Test]
        [Description("Класс эквивалентности: {case_desc}")]
        {test_cases}
        public void Test_{method_name}_{case_name}({method_params})
        {{
            // === Arrange ===
            // Предусловие (Precondition): {pre}
            // Ожидаемый результат: {expected}

            // === Act ===
{act_code}

            // === Assert ===
            // Постусловие (Postcondition): {post}
{assert_code}
        }}
"""

TEST_CASE_TEMPLATE = "[TestCase({inputs})]"

# ---------------------------------------------------------
# 2. ПАРСИНГ СПЕЦИФИКАЦИИ
# ---------------------------------------------------------
def load_spec(spec_path: str) -> Dict[str, Any]:
    """Безопасная загрузка YAML-спецификации."""
    with open(spec_path, "r", encoding="utf-8") as f:
        return yaml.safe_load(f)

# ---------------------------------------------------------
# 3. ГЕНЕРАЦИЯ КОДА
# ---------------------------------------------------------
def format_csharp_input(value: Any) -> str:
    """Преобразует значение из YAML в литерал C#."""
    if value is None:
        return "null"
    if isinstance(value, str):
        return f'"{value}"'
    if isinstance(value, bool):
        return "true" if value else "false"
    return str(value)

def generate_method_tests(method_data: Dict[str, Any]) -> List[str]:
    """Генерирует один метод теста с параметризацией для всех классов эквивалентности."""
    case_blocks = []
    for eq_class in method_data.get("equivalence_classes", []):
        # Формируем списки входных параметров для [TestCase]
        inputs_str = ", ".join(format_csharp_input(inp) for inp in eq_class["inputs"])
        
        # Простой парсинг параметров из signature для метода тестов
        sig = method_data.get("signature", "")
        params_str = sig[sig.find("(")+1:sig.rfind(")")] if "(" in sig else "string ipAddress"
        param_names = ", ".join([p.strip().split()[-1] for p in params_str.split(",") if p.strip()])
        
        method_name = method_data["name"]
        expected = eq_class["expected"]
        
        # Интеллектуальное формирование Act и Assert на основе expected
        if "Exception" in expected:
            act_code = f"            TestDelegate action = () => _sut.{method_name}({param_names});"
            assert_code = f"            Assert.Throws<{expected}>(action);"
        else:
            if method_data["signature"].startswith("void"):
                act_code = f"            _sut.{method_name}({param_names});"
                assert_code = "            Assert.Pass(\"Method completed successfully.\");"
            else:
                act_code = f"            var result = _sut.{method_name}({param_names});"
                # Если возвращает строку или bool
                assert_code = f"            Assert.That(result, Is.EqualTo({expected}));"

        # Собираем блок теста
        case_blocks.append(
            TEST_METHOD_TEMPLATE.format(
                case_desc=eq_class["case"],
                test_cases=TEST_CASE_TEMPLATE.format(inputs=inputs_str),
                method_name=method_name,
                case_name=eq_class["case"].replace(" ", "_").replace("(", "").replace(")", ""),
                method_params=params_str,
                pre=method_data.get("pre", "N/A"),
                expected=expected,
                act_code=act_code,
                post=method_data.get("post", "N/A"),
                assert_code=assert_code
            )
        )
    return case_blocks

def render_and_save(spec: Dict[str, Any], config: Dict[str, Any]) -> None:
    """Собирает полный файл тестов и сохраняет на диск."""
    module_name = spec["module"]
    test_methods = []
    
    for method in spec["methods"]:
        test_methods.extend(generate_method_tests(method))
        
    # Склеиваем методы в один блок
    tests_block = "".join(test_methods)
    
    # Рендерим файл
    file_content = TEST_FILE_TEMPLATE.format(
        spec_source=config.get("spec_path", "N/A"),
        module_name=module_name,
        test_methods=tests_block
    )
    
    # Сохраняем
    out_dir = Path(config.get("output_dir", "tests/Module.Tests"))
    out_dir.mkdir(parents=True, exist_ok=True)
    output_file = out_dir / f"{module_name}Tests.Generated.cs"
    
    output_file.write_text(file_content, encoding="utf-8")
    print(f"[✓] Сгенерирован файл: {output_file}")
    print(f"    Методов покрыто: {len(spec['methods'])}")
    print(f"    Тестов сгенерировано: {sum(len(m.get('equivalence_classes', [])) for m in spec['methods'])}")

# ---------------------------------------------------------
# 4. ТОЧКА ВХОДА
# ---------------------------------------------------------
if __name__ == "__main__":
    parser = argparse.ArgumentParser(description="C# NUnit Test Generator from YAML Spec")
    parser.add_argument("--config", default="config.yaml", help="Путь к config.yaml")
    args = parser.parse_args()

    print("[*] Загрузка конфигурации...")
    with open(args.config, "r", encoding="utf-8") as f:
        config = yaml.safe_load(f)

    print(f"[*] Загрузка спецификации: {config['spec_path']}...")
    spec_data = load_spec(config["spec_path"])

    print("[*] Генерация C# тестов...")
    render_and_save(spec_data, config)
    print("[✓] Готово.")
