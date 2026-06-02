// =============================================================
// AUTO-GENERATED TESTS. DO NOT EDIT MANUALLY.
// Source: spec/ipvalidator.yaml
// Generator: gen_tests.py v1.0
// =============================================================
using System;
using System.Collections.Generic;
using NUnit.Framework;
using Lab.Interfaces;
using Lab.Implementations;

namespace Module.Tests
{
    [TestFixture]
    [Description("Автоматически сгенерированные тесты для IpAddressValidator")]
    public class IpAddressValidatorTests
    {
        private IIpAddressValidator _sut;

        [SetUp]
        public void SetUp()
        {
            // Инициализация тестируемой системы (SUT)
            _sut = new IpAddressValidator();
        }


        [Test]
        [Description("Класс эквивалентности: Корректный IPv4 адрес")]
        [TestCase("192.168.1.1")]
        public void Test_IsValidIpv4_Корректный_IPv4_адрес(string ipAddress)
        {
            // === Arrange ===
            // Предусловие (Precondition): true
            // Ожидаемый результат: true

            // === Act ===
            var result = _sut.IsValidIpv4(ipAddress);

            // === Assert ===
            // Постусловие (Postcondition): Возвращает true, если ipAddress является корректным IPv4 адресом (4 октета от 0 до 255 без ведущих нулей и букв), иначе false
            Assert.That(result, Is.EqualTo(true));
        }

        [Test]
        [Description("Класс эквивалентности: Наличие букв в адресе")]
        [TestCase("not.an.ip")]
        public void Test_IsValidIpv4_Наличие_букв_в_адресе(string ipAddress)
        {
            // === Arrange ===
            // Предусловие (Precondition): true
            // Ожидаемый результат: false

            // === Act ===
            var result = _sut.IsValidIpv4(ipAddress);

            // === Assert ===
            // Постусловие (Postcondition): Возвращает true, если ipAddress является корректным IPv4 адресом (4 октета от 0 до 255 без ведущих нулей и букв), иначе false
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        [Description("Класс эквивалентности: Число в октете больше 255")]
        [TestCase("256.0.0.1")]
        public void Test_IsValidIpv4_Число_в_октете_больше_255(string ipAddress)
        {
            // === Arrange ===
            // Предусловие (Precondition): true
            // Ожидаемый результат: false

            // === Act ===
            var result = _sut.IsValidIpv4(ipAddress);

            // === Assert ===
            // Постусловие (Postcondition): Возвращает true, если ipAddress является корректным IPv4 адресом (4 октета от 0 до 255 без ведущих нулей и букв), иначе false
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        [Description("Класс эквивалентности: Пробелы в начале или конце")]
        [TestCase(" 192.168.1.1 ")]
        public void Test_IsValidIpv4_Пробелы_в_начале_или_конце(string ipAddress)
        {
            // === Arrange ===
            // Предусловие (Precondition): true
            // Ожидаемый результат: false

            // === Act ===
            var result = _sut.IsValidIpv4(ipAddress);

            // === Assert ===
            // Постусловие (Postcondition): Возвращает true, если ipAddress является корректным IPv4 адресом (4 октета от 0 до 255 без ведущих нулей и букв), иначе false
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        [Description("Класс эквивалентности: Передача null")]
        [TestCase(null)]
        public void Test_IsValidIpv4_Передача_null(string ipAddress)
        {
            // === Arrange ===
            // Предусловие (Precondition): true
            // Ожидаемый результат: false

            // === Act ===
            var result = _sut.IsValidIpv4(ipAddress);

            // === Assert ===
            // Постусловие (Postcondition): Возвращает true, если ipAddress является корректным IPv4 адресом (4 октета от 0 до 255 без ведущих нулей и букв), иначе false
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        [Description("Класс эквивалентности: Передача пустой строки")]
        [TestCase("")]
        public void Test_IsValidIpv4_Передача_пустой_строки(string ipAddress)
        {
            // === Arrange ===
            // Предусловие (Precondition): true
            // Ожидаемый результат: false

            // === Act ===
            var result = _sut.IsValidIpv4(ipAddress);

            // === Assert ===
            // Постусловие (Postcondition): Возвращает true, если ipAddress является корректным IPv4 адресом (4 октета от 0 до 255 без ведущих нулей и букв), иначе false
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        [Description("Класс эквивалентности: Ведущие нули в октете")]
        [TestCase("192.168.01.1")]
        public void Test_IsValidIpv4_Ведущие_нули_в_октете(string ipAddress)
        {
            // === Arrange ===
            // Предусловие (Precondition): true
            // Ожидаемый результат: false

            // === Act ===
            var result = _sut.IsValidIpv4(ipAddress);

            // === Assert ===
            // Постусловие (Postcondition): Возвращает true, если ipAddress является корректным IPv4 адресом (4 октета от 0 до 255 без ведущих нулей и букв), иначе false
            Assert.That(result, Is.EqualTo(false));
        }

        [Test]
        [Description("Класс эквивалентности: Корректность класса A")]
        [TestCase("10.0.0.1")]
        public void Test_GetIpClass_Корректность_класса_A(string ipAddress)
        {
            // === Arrange ===
            // Предусловие (Precondition): IsValidIpv4(ipAddress) == true
            // Ожидаемый результат: "A"

            // === Act ===
            var result = _sut.GetIpClass(ipAddress);

            // === Assert ===
            // Постусловие (Postcondition): Возвращает класс (A, B, C, D, E) для валидных адресов, выбрасывает ArgumentException для невалидных адресов или специальных блоков (например, 0 или 127)
            Assert.That(result, Is.EqualTo("A"));
        }

        [Test]
        [Description("Класс эквивалентности: Корректность класса B")]
        [TestCase("172.16.0.1")]
        public void Test_GetIpClass_Корректность_класса_B(string ipAddress)
        {
            // === Arrange ===
            // Предусловие (Precondition): IsValidIpv4(ipAddress) == true
            // Ожидаемый результат: "B"

            // === Act ===
            var result = _sut.GetIpClass(ipAddress);

            // === Assert ===
            // Постусловие (Postcondition): Возвращает класс (A, B, C, D, E) для валидных адресов, выбрасывает ArgumentException для невалидных адресов или специальных блоков (например, 0 или 127)
            Assert.That(result, Is.EqualTo("B"));
        }

        [Test]
        [Description("Класс эквивалентности: Корректность класса C")]
        [TestCase("192.168.1.1")]
        public void Test_GetIpClass_Корректность_класса_C(string ipAddress)
        {
            // === Arrange ===
            // Предусловие (Precondition): IsValidIpv4(ipAddress) == true
            // Ожидаемый результат: "C"

            // === Act ===
            var result = _sut.GetIpClass(ipAddress);

            // === Assert ===
            // Постусловие (Postcondition): Возвращает класс (A, B, C, D, E) для валидных адресов, выбрасывает ArgumentException для невалидных адресов или специальных блоков (например, 0 или 127)
            Assert.That(result, Is.EqualTo("C"));
        }

        [Test]
        [Description("Класс эквивалентности: Исключение для невалидного IP")]
        [TestCase("999.9.9.9")]
        public void Test_GetIpClass_Исключение_для_невалидного_IP(string ipAddress)
        {
            // === Arrange ===
            // Предусловие (Precondition): IsValidIpv4(ipAddress) == true
            // Ожидаемый результат: ArgumentException

            // === Act ===
            TestDelegate action = () => _sut.GetIpClass(ipAddress);

            // === Assert ===
            // Постусловие (Postcondition): Возвращает класс (A, B, C, D, E) для валидных адресов, выбрасывает ArgumentException для невалидных адресов или специальных блоков (например, 0 или 127)
            Assert.Throws<ArgumentException>(action);
        }

        [Test]
        [Description("Класс эквивалентности: Исключение для loopback адреса")]
        [TestCase("127.0.0.1")]
        public void Test_GetIpClass_Исключение_для_loopback_адреса(string ipAddress)
        {
            // === Arrange ===
            // Предусловие (Precondition): IsValidIpv4(ipAddress) == true
            // Ожидаемый результат: ArgumentException

            // === Act ===
            TestDelegate action = () => _sut.GetIpClass(ipAddress);

            // === Assert ===
            // Постусловие (Postcondition): Возвращает класс (A, B, C, D, E) для валидных адресов, выбрасывает ArgumentException для невалидных адресов или специальных блоков (например, 0 или 127)
            Assert.Throws<ArgumentException>(action);
        }

    }
}
