using ClassLibrary;

namespace ClassLibrary.Tests
{
    public class PhoneTests
    {
        [TestClass]
        public sealed class Test1
        {
            [TestMethod]
            public void Test_Konstruktor_dane_poprawne_działanie_ok()
            {
                //AA

                //arrange
                var wlasciciel = "Molenda";
                var numerTelefonu = "123456789";
                //act
                var phone = new Phone(wlasciciel, numerTelefonu);

                //Assert
                Assert.AreEqual(wlasciciel, phone.Owner);
                Assert.AreEqual(numerTelefonu, phone.PhoneNumber);
            }


            [TestMethod]
        public void Konstruktor_Powinien_Rzucic_Wyjatkiem_Gdy_Wlasciciel_Jest_Null_Lub_Pusty()
        {
            var numerTelefonu = "123456789";

            Assert.Throws<ArgumentException>(() => new Phone(null, numerTelefonu));
            Assert.Throws<ArgumentException>(() => new Phone(string.Empty, numerTelefonu));
        }

        [TestMethod]
        public void Konstruktor_Powinien_Rzucic_Wyjatkiem_Gdy_Numer_Telefonu_Jest_Nieprawidlowy()
        {
            var wlasciciel = "Jan Kowalski";

            Assert.Throws<ArgumentException>(() => new Phone(wlasciciel, null));
            Assert.Throws<ArgumentException>(() => new Phone(wlasciciel, string.Empty));
            Assert.Throws<ArgumentException>(() => new Phone(wlasciciel, "12345"));
            Assert.Throws<ArgumentException>(() => new Phone(wlasciciel, "1234567890"));
            Assert.Throws<ArgumentException>(() => new Phone(wlasciciel, "12345678a"));
        }

        [TestMethod]
        public void DodajKontakt_Powinien_Dodac_Kontakt_Gdy_Ksiazka_Telefoniczna_Nie_Jest_Pelna()
        {
            var telefon = new Phone("Jan Kowalski", "123456789");
            var nazwaKontaktu = "Anna Nowak";
            var numerKontaktu = "987654321";

            var wynik = telefon.AddContact(nazwaKontaktu, numerKontaktu);

            Assert.True(wynik);
            Assert.Equal(1, telefon.Count);
        }

        [TestMethod]
        public void DodajKontakt_Nie_Powinien_Dodac_Kontaktu_Gdy_Kontakt_Juz_Istnieje()
        {
            var telefon = new Phone("Jan Kowalski", "123456789");
            var nazwaKontaktu = "Anna Nowak";
            var numerKontaktu = "987654321";
            telefon.AddContact(nazwaKontaktu, numerKontaktu);

            var wynik = telefon.AddContact(nazwaKontaktu, numerKontaktu);

            Assert.False(wynik);
            Assert.Equal(1, telefon.Count);
        }

        [TestMethod]
        public void DodajKontakt_Powinien_Rzucic_Wyjatkiem_Gdy_Ksiazka_Telefoniczna_Jest_Pelna()
        {
            var telefon = new Phone("Jan Kowalski", "123456789");
            for (int i = 0; i < telefon.PhoneBookCapacity; i++)
            {
                telefon.AddContact($"Kontakt{i}", "987654321");
            }

            Assert.Throws<InvalidOperationException>(() => telefon.AddContact("Nowy Kontakt", "987654321"));
        }

        [TestMethod]
        public void Zadzwon_Powinien_Zwrocic_Prawidlowa_Wiadomosc_Gdy_Kontakt_Istnieje()
        {
            var telefon = new Phone("Jan Kowalski", "123456789");
            var nazwaKontaktu = "Anna Nowak";
            var numerKontaktu = "987654321";
            telefon.AddContact(nazwaKontaktu, numerKontaktu);

            var wynik = telefon.Call(nazwaKontaktu);

            Assert.Equal($"Calling {numerKontaktu} ({nazwaKontaktu}) ...", wynik);
        }

        [TestMethod]
        public void Zadzwon_Powinien_Rzucic_Wyjatkiem_Gdy_Kontakt_Nie_Istnieje()
        {
            var telefon = new Phone("Jan Kowalski", "123456789");

            Assert.Throws<InvalidOperationException>(() => telefon.Call("Nieistniejacy Kontakt"));
        }
    }
}
