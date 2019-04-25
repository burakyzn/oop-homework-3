using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace odev3
{
    class Hesaplama
    {
        bool hata;
        bool ilkKarakterSayiDegil;
        char ilkKarakter;
        double toplam = 0;

        string hesaplanacak;
        string[] bolunmusKatsayilar;

        List<double> katsayilarim;
        List<char> operatorler;

        public Hesaplama()
        {
            // sinifin degiskenlerine ilk degerleri atandi.
            hata = true;
            ilkKarakter = '+';
            ilkKarakterSayiDegil = false;
            hesaplanacak = "";
            toplam = 0;
        }

        public void HesapYap()
        {
            // bu fonksiyonun islevi diger fonksiyonlari cagiran bir cati gorevi gormek
            // diger fonksiyonlarımız parca parca cagrilamamasi icin private
            DenklemAl();
            DenklemiParcala();
            Carpma();
            Bolme();
            Toplama();
            SonucYazdir();
        }

        private void DenklemAl()
        {
            // bu fonksiyon kullanicidan denklemi dogru alana kadar calisacak
            while (hata)
            {
                hata = false;
                Console.WriteLine("Hesaplanacak denklemi giriniz :");
                hesaplanacak = Console.ReadLine();

                // alinan denklemin son karakteri bolme carpma toplama cikarma isareti olamaz.
                // alinan denklemde isaret ile mi baslanmis ona dikkat ediliyor.
                for (int i = 0; i < hesaplanacak.Length; i++)
                {
                    if (i == 0 && Char.IsDigit(hesaplanacak[i]) == false)
                    {
                        ilkKarakter = hesaplanacak[0];
                        ilkKarakterSayiDegil = true;
                    }

                    if (i == hesaplanacak.Length - 1 && Char.IsDigit(hesaplanacak[i]) == false)
                        hata = true;
                    if ((Char.IsDigit(hesaplanacak[i]) == false) && (Char.IsDigit(hesaplanacak[i + 1]) == false))
                        hata = true;
                }

                if (hata == true)
                {
                    // denklem kurallara uymuyorsa tekrar almak için hata mesaji yazdirip donguden cikmiyoruz.
                    Console.WriteLine("Hatali islem yapildi lutfen tekrar deneyin.");
                }
            }
        }

        private void DenklemiParcala()
        {
            // kullanicidan gelen denklemi operatorler ile ayiriyoruz.
            bolunmusKatsayilar = hesaplanacak.Split('*', '/', '+', '-');
            katsayilarim = new List<double>();

            // operatorlerle ayirinca geriye sadece sayilar kalıyor bunları double seklinde bir listeye aktariyorum.
            if (ilkKarakterSayiDegil == true)
            {
                for (int i = 1; i < bolunmusKatsayilar.Length; i++)
                {
                    katsayilarim.Add(double.Parse(bolunmusKatsayilar[i]));
                }
            }
            else
            {
                foreach (string s in bolunmusKatsayilar)
                {
                    katsayilarim.Add(double.Parse(s));
                }
            }

            // operatorleride ayri bir char listesinde tutuyorum.
            operatorler = new List<char>();

            foreach (char karakter in hesaplanacak)
            {
                if (Char.IsDigit(karakter) == false)
                {
                    operatorler.Add(karakter);
                }
            }

            // ilk karakteri baska bir degiskende tuttugum icin operator listesinde olmasıan gerek yok siliyoruz.
            if (ilkKarakterSayiDegil == true && ilkKarakter == '-')
                operatorler.RemoveAt(0);
            else if (ilkKarakterSayiDegil == true && ilkKarakter == '+')
            {
                operatorler.RemoveAt(0);
            }
        }

        private void Carpma()
        {
            // burada denklemdeki carpma islemlerine bakilacak ve gerekli islemler yapilacak
            // carpma islemi yapildikca bulunan sonuc denklemde aynı yerine + olarak yazilacak.
            int carpmaOperatorSayisi = 0;
            for (int i = 0; i < operatorler.Count; i++)
            {
                if (operatorler[i] == '*')
                {
                    // operatorler listesindeki carpma ifadesinin katsayilardaki karsiliginin sag ve sol kisimlerini deger olarak aldim.
                    double solDeger = Convert.ToDouble(katsayilarim[i - carpmaOperatorSayisi]);
                    double sagDeger = Convert.ToDouble(katsayilarim[i - carpmaOperatorSayisi + 1]);
                    double carpim = sagDeger * solDeger;

                    // artik carpilan degerler katsayilardan silinebilir. toplam carpilmis deger oraya yazilacaktir.
                    katsayilarim.RemoveAt(i - carpmaOperatorSayisi);
                    katsayilarim.RemoveAt(i - carpmaOperatorSayisi);
                    katsayilarim.Insert(i - carpmaOperatorSayisi, carpim);
                    carpmaOperatorSayisi++;
                }
            }
            for (int i = 0; i < carpmaOperatorSayisi; i++)
                operatorler.Remove('*');
            // carpmanin isi bittigi icin operator listemdeki tum carpilari siliyorum.
        }

        private void Bolme()
        {
            // burada denklemdeki bolme islemlerine bakilacak ve gerekli islemler yapilacak
            // bolme islemi yapildikca bulunan sonuc denklemde aynı yerine + olarak yazilacak.
            int bolmeOperatorSayisi = 0;
            for (int i = 0; i < operatorler.Count; i++)
            {
                if (operatorler[i] == '/')
                {
                    // operatorler listesindeki bolme ifadesinin katsayilardaki karsiliginin sag ve sol kisimlerini deger olarak aldim.
                    double solDeger = Convert.ToDouble(katsayilarim[i - bolmeOperatorSayisi]);
                    double sagDeger = Convert.ToDouble(katsayilarim[i - bolmeOperatorSayisi + 1]);
                    double bolum = solDeger / sagDeger;

                    // artik bolunen degerler katsayilardan silinebilir. toplam bolunmus deger oraya yazilacaktir.
                    katsayilarim.RemoveAt(i - bolmeOperatorSayisi);
                    katsayilarim.RemoveAt(i - bolmeOperatorSayisi);
                    katsayilarim.Insert(i - bolmeOperatorSayisi, bolum);
                    bolmeOperatorSayisi++;
                }
            }
            for (int i = 0; i < bolmeOperatorSayisi; i++)
                operatorler.Remove('/');
            // bolmenin isi bittigi icin operator listemdeki tum carpilari siliyorum.
        }

        private void Toplama()
        {
            // yukarida carpma ve bolme yaparken cikan sonucların hepsini + seklinde yazmistik.
            // - de bir aslinda toplama oldugu icin tekrardan cikarmaya bakmamiza gerek yok
            // geri kalan butun degerleri + - olmasına gore topluyoruz.
            if (ilkKarakter == '+')
                toplam += katsayilarim[0];
            else
                toplam -= katsayilarim[0];

            for (int i = 0; i < operatorler.Count; i++)
            {
                if (operatorler[i] == '-')
                    toplam -= katsayilarim[i + 1];
                else
                    toplam += katsayilarim[i + 1];
            }
        }

        private void SonucYazdir()
        {
            // son olarak ekrana toplam yazildi.
            Console.WriteLine("Sonuc : " + toplam);
        }
    }
}
