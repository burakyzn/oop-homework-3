using System;
using System.Collections.Generic;

namespace odev3
{
    class Program
    {
        static void Main(string[] args)
        {
            bool hata = true;
            char ilkKarakter = '+';
            string hesaplanacak = "";
            while (hata)
            {
                hata = false;
                Console.WriteLine("Hesaplanacak denklemi giriniz :");
                hesaplanacak = Console.ReadLine();

                for (int i = 0; i < hesaplanacak.Length; i++)
                {
                    if (i == 0 && Char.IsDigit(hesaplanacak[i]) == false)
                        ilkKarakter = hesaplanacak[i];
                    if (i == hesaplanacak.Length - 1 && Char.IsDigit(hesaplanacak[i]) == false)
                        hata = true;
                    if ((Char.IsDigit(hesaplanacak[i]) == false) && (Char.IsDigit(hesaplanacak[i + 1]) == false))
                        hata = true;            
                }

                if (hata == true)
                {
                    Console.WriteLine("Hatali islem yapildi lutfen tekrar deneyin.");
                }
            }

            string[] bolunmusKatsayilar = hesaplanacak.Split('*', '/', '+', '-');
            List<double> katsayilarim = new List<double>();
            foreach (string s in bolunmusKatsayilar)
                katsayilarim.Add(double.Parse(s));

            List<char> operatorler = new List<char>();

            foreach (char karakter in hesaplanacak)
            {
                if (Char.IsDigit(karakter) == false)
                {
                    operatorler.Add(karakter);
                }
            }

            if (ilkKarakter == '-')
                operatorler.RemoveAt(0);

            int carpmaOperatorSayisi = 0;
            for (int i = 0; i < operatorler.Count; i++)
            {
                if (operatorler[i] == '*')
                {
                    double solDeger = Convert.ToDouble(katsayilarim[i - carpmaOperatorSayisi]);
                    double sagDeger = Convert.ToDouble(katsayilarim[i - carpmaOperatorSayisi + 1]);
                    double carpim = sagDeger * solDeger;

                    katsayilarim.RemoveAt(i - carpmaOperatorSayisi);
                    katsayilarim.RemoveAt(i - carpmaOperatorSayisi);
                    katsayilarim.Insert(i - carpmaOperatorSayisi, carpim);
                    carpmaOperatorSayisi++;
                }
            }
            for (int i = 0; i < carpmaOperatorSayisi; i++)
                operatorler.Remove('*');

            int bolmeOperatorSayisi = 0;
            for (int i = 0; i < operatorler.Count; i++)
            {
                if (operatorler[i] == '/')
                {
                    double solDeger = Convert.ToDouble(katsayilarim[i - bolmeOperatorSayisi]);
                    double sagDeger = Convert.ToDouble(katsayilarim[i - bolmeOperatorSayisi + 1]);
                    double bolum = solDeger / sagDeger;

                    katsayilarim.RemoveAt(i - bolmeOperatorSayisi);
                    katsayilarim.RemoveAt(i - bolmeOperatorSayisi);
                    katsayilarim.Insert(i - bolmeOperatorSayisi, bolum);
                    bolmeOperatorSayisi++;
                }
            }
            for (int i = 0; i < bolmeOperatorSayisi; i++)
                operatorler.Remove('/');

            double toplam = 0;
            if (ilkKarakter == '+')
                toplam += katsayilarim[0];
            else
                toplam -= katsayilarim[0];

            for(int i = 0; i < operatorler.Count; i++)
            {
                if (operatorler[i] == '-')
                    toplam -= katsayilarim[i+1];
                else
                    toplam += katsayilarim[i+1];
            }

            Console.WriteLine("Sonuc : " + toplam);

        }

    }
}
