#  RPN Calculator (Reverse Polish Notation Hesap Makinesi)

Bu proje, C# ile geliştirilmiş bir **RPN (Reverse Polish Notation)** hesap makinesidir.  
Kullanıcıdan postfix formatında ifadeler alır, özel olarak tasarlanmış yığın (stack) yapısını kullanarak sonucu hesaplar.  

---

## içindekiler
- [Özellikler](#özellikler)
- [Kurulum](#kurulum)
- [Kullanım Örnekleri](#kullanım-örnekleri)
- [Sınıf Yapısı](#sınıf-yapısı)
- [Hatalar ve Loglama](#hatalar-ve-loglama)
- [Lisans](#lisans)
- [Nasıl Geliştirilebilir](#nasıl-geliştirilebilir)





---

##  Özellikler
-  Toplama (`+`), çıkarma (`-`), çarpma (`*`), bölme (`/`) işlemleri  
-  Hatalı girişlerde özel hata mesajları  
-  Sıfıra bölme hatası kontrolü  
-  Konsol tabanlı kullanıcı arayüzü  
-  Hata loglama (`calculator_errors.log` dosyasına kaydedilir)  
-  Nesne yönelimli tasarım (OOP): Operand, Operator, Stack, Calculator, GUI sınıfları  

---

##  Kurulum

Projeyi bilgisayarınızda çalıştırmak için aşağıdaki adımları takip edin:  

1. **Depoyu klonlayın**  
   ```bash
   git clone https://github.com/kullanici/rpn-calculator.git
   ```
2. **Proje klasörüne gidin**
   ```bash
   cd rpn-calculator
   ```
3. **.NET SDK’yı kontrol edin**
   Bilgisayarınızda .NET 6.0 veya daha güncel bir sürüm kurulu olmalı. Kontrol için: 
   ```bash
   dotnet --version
   ```
4. **Projeyi derleyip çalıştırın**
   ```bash
   dotnet build
   dotnet run
   ```
5. **Alternatif olarak Visual Studio kullanabilirsiniz**
-  Proje dosyasını Visual Studio’da açın

-  Çözümü derleyin (Build Solution)

-  Programı başlatın (Start Without Debugging → Ctrl + F5)

---

## Kullanım Örnekleri
### Adım Adım Kullanım:
1. Programı çalıştırın (`dotnet run` veya Visual Studio’dan başlatın).  
2. Konsolda RPN formatında bir ifade girin.  
   - Örnek: `3 4 +` → 3 + 4 = 7  
   - Örnek: `5 1 2 + 4 * + 3 -` → 14  
3. Girilen ifadeyi değerlendirmek için **Enter** tuşuna basın.  
4. Hesaplanan sonuç ekranda gösterilecektir.  
5. Programdan çıkmak için `cikis` yazıp Enter’a basın.  
```bash
Input: 5 1 2 + 4 * + 3 -
Output: 14

Input: 3 4 2 + -
Output: -3

Input: 10 2 /
Output: 5
```
---
## Sınıf Yapısı
```bash
Program.cs     -> Uygulamanın başlangıç noktası
Calculator.cs  -> Hesaplama işlemlerini yapan sınıf
Logger.cs      -> Hataları "calculator_errors.log" dosyasına kaydeden sınıf

Yeni operatör eklemek için:
Calculator.cs içerisine yeni bir case eklemen yeterli.
```
---

##  Hatalar ve Loglama

RPN Calculator, kullanıcı hatalarına ve hesaplama sorunlarına karşı önlemler içerir:

1. **Hatalı girişler**  
   - Sayılar ve operatörler dışında karakter girilirse:
   ```bash
   Input: 3 4 a +
   Output: HATA: Tanımlanmamış operatör: a
   ```
2. **Eksik operand**
   - Operatör için yeterli sayı yoksa:
   ```bash
   Input: 3 +
   Output: HATA: '+' operatörü için yeterli operand yok!
   ```
3. **Sıfıra bölme**
   - Bölme işleminde sıfır ile bölme hatası kontrol edilir:
   ```bash
   Input: 10 0 /
   Output: HATA: Sıfıra bölme hatası!
   ```
4. **Loglama**
   - Tüm hatalar calculator_errors.log dosyasına kaydedilir.
   - Log formatı:
   ```bash
   [Tarih/Saat] Girdi: '...' - Hata: 'Hata mesajı'
   ```
  

---

## Lisans
<details>
<summary>LICENSE</summary>

**Bu proje MIT Lisansı ile paylaşılmıştır.**

**Detaylar için LICENSE dosyasına bakabilirsiniz.**

</details>


   
---

##  Nasıl Geliştirilebilir

Projeyi temel alarak yeni özellikler eklemek oldukça kolaydır. İşte öneriler:

1. **Yeni operatör eklemek**  
   - `Calculator.cs` dosyasındaki `Operator` sınıfını genişletebilirsiniz.  
   - Örneğin üs alma, mod alma gibi işlemler için yeni bir sınıf oluşturup `Calculate` metodunu implement edin.  
   - Ardından `operators` dictionary’sine ekleyin:
   ```csharp
   operators["^"] = new UsOperator(); // Örnek
   ```
2. **GUI eklemek**

 - Konsol arayüz yerine Windows Forms veya WPF ile görsel arayüz ekleyebilirsiniz.

 - HesapMakArayuz sınıfı temel alınıp, GUI bileşenleriyle değiştirilip genişletilebilir.
3. **Hata kontrolünü geliştirmek**
 - Mevcut loglama sistemi korunabilir.

 - Daha detaylı kullanıcı hataları veya input doğrulama eklenebilir.
4. **Unit test eklemek**
 - Calculator sınıfının her operatörü için unit test yazarak mevcut kodun bozulmamasını garanti edebilirsiniz.
   
**Kod OOP prensiplerine göre tasarlandığı için, sınıflar birbirinden bağımsızdır. Bu sayede bir modülü değiştirseniz bile diğer kısımlar bozulmaz.**

---

## Katkıda Bulunma
1. Bu repoyu forkla
2. Yeni bir branch oluştur (feature/yeni-ozellik)
3. Değişikliklerini yap
4. Pull request gönder

---

##  İletişim

Projeyle ilgili sorularınız, hata bildirimleri veya önerileriniz için aşağıdaki yolları kullanabilirsiniz:

- **LinkedIn**: [Efe Ayyıldız](https://linkedin.com/in/efeayyildiz)  

Lütfen proje ile ilgili her türlü sorunu veya öneriyi GitHub Issues üzerinden bildirin; bu, hem sizin hem de diğer kullanıcıların daha hızlı ve düzenli çözüm almasını sağlar.

---
