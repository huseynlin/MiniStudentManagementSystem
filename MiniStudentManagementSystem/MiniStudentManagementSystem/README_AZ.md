# Mini Tələbə İdarəetmə Sistemi

Tələbə qeydlərini idarə etmək üçün sadə ASP.NET WebForms proyekti. Bu akademik tərzdə yaradılan tətbiqdir.

## Layihəyə Ümumi Baxış

Bu layihə istifadəçi qeydiyyatı, daxil olmaq və tələbə qeydləri üzərində CRUD əməliyyatları ilə tələbə idarəetmə sistemi həyata keçirir. Məlumatlar sadəlik üçün JSON fayllarında saxlanılır.

## Xüsusiyyətlər

### 1. İstifadəçi Qeydiyyatı (Register.aspx)
- İstifadəçilər İstifadəçi adı, Elektron poçt və Şifrə ilə hesab yarada bilərlər
- Dublikat istifadəçi adı doğrulaması
- Sadə xəta və uğur mesajları
- İstifadəçilər `App_Data/users.json` faylında saxlanılır

### 2. İstifadəçi Daxil Olması (Login.aspx)
- İstifadəçilər İstifadəçi adı və Şifrə ilə daxil ola bilərlər
- Sessiya İdarəetməsi (`Session["username"]`)
- 30 dəqiqə müddətində HttpCookie
- Uğurlu daxil olmada Tələbələr səhifəsinə yönləndir

### 3. Tələbə İdarəetməsi (Students.aspx)
- Bütün tələbələri GridView-də göstər
- Ad, Soyad və Tələbə Nömrəsi ilə yeni tələbə əlavə et
- Tələbə məlumatını redaktə et (GridView Edit/Update)
- Tələbəni sil (GridView Delete)
- Dublikat Tələbə Nömrəsi doğrulaması
- Giriş nəzarəti: Daxil olmaq lazımdır

### 4. Çıxış (Logout.aspx)
- Sessiyasını dayandır
- Autentifikasiya kukiləsini vaxtını bitir
- Daxil ol səhifəsinə yönləndir

## Layihə Yapısı

```
MiniStudentManagementSystem/
├── Register.aspx                   # Qeydiyyat səhifəsi
├── Register.aspx.cs                # Qeydiyyat kodu
├── Register.aspx.designer.cs       # Dizayn faylı
├── Login.aspx                      # Daxil ol səhifəsi
├── Login.aspx.cs                   # Daxil ol kodu
├── Login.aspx.designer.cs          # Dizayn faylı
├── Students.aspx                   # Tələbə idarəetməsi səhifəsi
├── Students.aspx.cs                # Tələbə kodu
├── Students.aspx.designer.cs       # Dizayn faylı
├── Logout.aspx                     # Çıxış səhifəsi
├── Logout.aspx.cs                  # Çıxış kodu
├── Logout.aspx.designer.cs         # Dizayn faylı
├── Default.aspx                    # Başlanğıc səhifə
├── Default.aspx.cs                 # Başlanğıc kodu
├── Default.aspx.designer.cs        # Dizayn faylı
├── Models/
│   ├── User.cs                     # İstifadəçi model sinfı
│   └── Student.cs                  # Tələbə model sinfı
├── App_Data/
│   ├── users.json                  # İstifadəçi məlumat saxlanması
│   └── students.json               # Tələbə məlumat saxlanması
└── Web.config                      # Konfigirasiya faylı
```

## Model Siniflər

### User.cs
```csharp
public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}
```

### Student.cs
```csharp
public class Student
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string StudentNumber { get; set; }
}
```

## Əsas Texnologiyalar

- **Çərçivə**: ASP.NET WebForms (.NET Framework 4.7.2)
- **Məlumat Saxlanması**: `System.Web.Script.Serialization.JavaScriptSerializer` ilə JSON faylları
- **Fayl İşləməsi**: `System.IO` ilə JSON fayllarının oxunması/yazılması
- **Sessiya İdarəetməsi**: ASP.NET Session və HttpCookie
- **UI Kontrolleri**: TextBox, Button, Label, GridView
- **Məlumat Bağlanması**: Avtomatik Edit/Delete düymələri ilə GridView

## İstifadə Etmə

### 1. Tətbiqin Başladılması
- `Default.aspx` ev səhifəsi kimi naviqasiya edin
- Daxil olmaq və ya Qeydiyyatdan keçmək seçənəklərini görəcəksiniz

### 2. Yeni Hesab Qeydiyyatı
1. Ev səhifəsində "Qeydiyyat" düyməsinə klikləyin
2. İstifadəçi adı, Elektron poçt və Şifrə daxil edin
3. "Qeydiyyatdan keç" düyməsinə klikləyin
4. Uğurlu olarsa, daxil olmağa davam edə bilərsiz

### 3. Daxil Olmaq
1. Ev səhifəsində "Daxil ol" düyməsinə klikləyin
2. İstifadəçi adı və Şifrə daxil edin
3. "Daxil ol" düyməsinə klikləyin
4. Uğurlu daxil olmada Tələbələr səhifəsinə yönləndirilərsiniz

### 4. Tələbələri İdarə Etmək
1. Daxil olduktan sonra Tələbələr səhifəsinə keçirsiniz
2. **Tələbə Əlavə Et**: Ad, Soyad, Tələbə Nömrəsi daxil edin və "Tələbə Əlavə Et" düyməsinə klikləyin
3. **Tələbəni Redaktə Et**: GridView-in hər hansı sırasında "Redaktə Et" düyməsinə klikləyin, dəyərləri dəyişdirin, "Yənilə" düyməsinə klikləyin
4. **Tələbəni Sil**: Tələbəni silmək üçün hər hansı sırada "Sil" düyməsinə klikləyin
5. **Çıxış**: Sağ üstdəki "Çıxış" keçidini klikləyin

## Fayl Quruluşu Problemi və Həlli

### Xəta: "Yola erişim engellendi" (Access to path denied)

**Səbəblər:**
1. App_Data qovluğu mövcud deyil
2. Fayl yazma icazəsi yoxdur
3. Yol səhvdir

**Həlli:**
- Kodda `Directory.CreateDirectory()` istifadə edərək avtomatik qovluq yaranır
- `Path.Combine()` istifadə edərək yollar düzgün birləşdirilir
- Bütün fayl əməliyyatları try-catch blokunda qorunur

## Kodun Xüsusiyyətləri

### Güvenlik Qeydləri
- Şifrələr düz mətində saxlanılır (yalnız akademik məqsədlər üçün)
- İstehsalda şifrələr hash və salt ilə saxlanmalıdır
- Bu layihə öyrənmə məqsədi ilə nəzərdə tutulmuşdur, istehsalda istifadə etmə

### JSON Fayl Əməliyyatları
- Fayllar `~/App_Data/` qovluğunda saxlanılır
- Boş JSON faylları `[]` olaraq başlanır
- Hər əməliyyat (Əlavə, Redaktə, Sil) bütün JSON faylını oxuyur, dəyişdirir və yadda saxlayır
- Fayllar `System.IO.File` sinfini istifadə edərək oxunur/yazılır

### GridView CRUD
- GridView-in `AutoGenerateEditButton` və `AutoGenerateDeleteButton` fəallaşdırılmışdır
- Hadisə işləyiciləri: `RowEditing`, `RowUpdating`, `RowCancelingEdit`, `RowDeleting`
- Hər əməliyyatdan sonra GridView JSON faylından yenilənir

### Doğrulama
- Qeydiyyat zamanı istifadəçi adı unikal olmalıdır
- Tələbə əlavə etmə/redaktə zamanı Tələbə Nömrəsi unikal olmalıdır
- Bütün giriş sahələri boşluq üçün doğrulanır

## Nümunə Məlumatlar

İlkin students.json 5 nümunə tələbə ilə ehtiva edir:
- Ahmet Yilmaz (2023001)
- Fatima Demir (2023002)
- Emre Kaya (2023003)
- Ayse Ozturk (2023004)
- Mehmet Arslan (2023005)

## Öyrənmə Konsepsiyaları

1. **ASPX Səhifələri**: Server kontrollləri ilə HTML formu
2. **Code-Behind (.aspx.cs)**: Markup-dan ayrılmış C# məntiqi
3. **Page_Load Hadisəsi**: Səhifə həyat dövrü işləməsi
4. **IsPostBack**: İlk yükləmə ilə postback-ı ayırmaq
5. **Session**: Autentifikasiya üçün `Session["username"]`
6. **HttpCookie**: Kuki yaratılması və müddəti bitməsi
7. **Server.MapPath**: App_Data qovluğuna giriş
8. **System.IO**: Fayl oxunması və yazılması
9. **JavaScriptSerializer**: JSON serialization/deserialization
10. **List<T>**: Obyekt məcmuəsi
11. **LINQ**: Siyahılar üçün sorğu (`FirstOrDefault()`)
12. **GridView**: Məlumat bağlanması və CRUD əməliyyatları
13. **Try-Catch**: Xəta işləməsi

## Test Addımları

1. **İstifadəçi Qeydiyyatı**
   - İstifadəçi adı: testuser
   - Elektron poçt: test@example.com
   - Şifrə: password123
   - App_Data/users.json-da doğrulayın

2. **Dublikat Qeydiyyatı Sınayın**
   - Eyni istifadəçi adı ilə qeydiyyatdan keçməyi sınayın
   - Xəta mesajı göstərilməlidir

3. **Kimlik məlumatları ilə Daxil Olun**
   - testuser / password123 ilə daxil olun
   - Tələbələr səhifəsinə yönləndirilməlisiniz

4. **Tələbə Əlavə Edin**
   - Ad: Fərid
   - Soyad: Həsənov
   - Tələbə Nömrəsi: 2023999
   - Əlavə Et düyməsinə klikləyin və GridView-də doğrulayın

5. **Tələbəni Redaktə Edin**
   - Hər hansı tələbədə "Redaktə Et" düyməsinə klikləyin
   - Dəyərləri dəyişdirin
   - "Yənilə" düyməsinə klikləyin
   - GridView-də dəyişiklikləri doğrulayın

6. **Tələbəni Silin**
   - Hər hansı tələbədə "Sil" düyməsinə klikləyin
   - Tələbənin silindiğini doğrulayın

7. **Çıxış Edin**
   - "Çıxış" keçidini klikləyin
   - Daxil ol səhifəsinə yönləndirilməlisiniz
   - Sessiya təmizlənməlidir

## Qeydlər

- Bu başlangıc səviyyəsi akademik proyektidir
- Heç bir qabaqcıl arxitektur nümunəsi yoxdur (depositorium, xidmətlər və s.)
- async/await istifadəsi yoxdur (sinxron əməliyyatlar)
- Sadə və oxunabilir kod tərzi
- Məntiqi izah edən şərhlər
- Məlumatlar yalnız App_Data qovluğunda qaldığı müddətcə saxlanılır

## Problemlərin Həlli

### "Fayl tapılmadı" xətası
- App_Data qovluğunun mövcud olduğundan əmin olun
- Koddakı fayl yollarını yoxlayın (Server.MapPath istifadə edərək)

### GridView yeniləmir
- JSON faylının düzgün oxunduğunu doğrulayın
- Məlumat yüklündükdən sonra DataBind() çağrıldığını yoxlayın

### Daxil olmaq işləmir
- users.json-un qeydiyyat ilə yaradıldığını doğrulayın
- İstifadəçi adı və şifrə tam olaraq uyğun gəldiğini yoxlayın
- Girişlərdə əlavə boşluq olub-olmadığını yoxlayın

### Sessiya tez bitər
- Web.config sessionState parametrlərini yoxlayın
- Kuki daxil olmadan 30 dəqiqə sonra bitir
