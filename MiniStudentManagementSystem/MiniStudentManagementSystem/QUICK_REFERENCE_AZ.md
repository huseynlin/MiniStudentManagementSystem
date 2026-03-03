# ⚡ Tez Referans Bələdçi

## 🚀 Tez Başlangıç (2 Dəqiqə)

```
1. Proyekti açın
2. F5 basın
3. Default.aspx görsünüz
4. "Qeydiyyat" → yeni hesap yaratın
5. "Daxıl ol" → daxıl olun
6. Tələbələri idarə edin
7. "Çıxış" → çıxın
```

## 📝 Qeydiyyat Forması

| Sahə | Tələb | Misal |
|------|--------|--------|
| İstifadəçi adı | 3-20 simvol | testuser |
| Elektron poçt | Etibarlı email | test@example.com |
| Şifrə | Minimum 6 simvol | password123 |

## 🔐 Daxıl Ol Forması

| Sahə | Nöte |
|------|------|
| İstifadəçi adı | Qeydiyyatda istifadə etdiyiniz ad |
| Şifrə | Dəqiq olaraq qeydiyyatda istifadə etdiyiniz şifrə |

## 📊 Tələbə Məlumatları

| Sahə | Format | Misal |
|------|--------|--------|
| Ad | Mətn | Əhməd |
| Soyad | Mətn | Həsənov |
| Tələbə Nömrəsi | Unikal | 2023001 |

## ✅ Doğrulama Qaydaları

### Qeydiyyat
- ❌ Boş sahə → Xəta
- ❌ Mövcud istifadəçi adı → Xəta
- ✅ Bütün sahələr doldurulmuş → Uğur

### Daxıl Olmaq
- ❌ Yanlış şifrə → Xəta
- ❌ Mövcud olmayan istifadəçi → Xəta
- ✅ Düzgün kimlik → Sessiya yaradılır

### Tələbə Əlavə Etmə
- ❌ Boş sahə → Xəta
- ❌ Mövcud nömrə → Xəta
- ✅ Bütün sahələr unikal → Əlavə edilir

## 🛠️ Xəta Mesajları

| Mesaj | Səbəb | Həlli |
|-------|--------|--------|
| "Lütfən bütün sahələri doldurun" | Boş sahə | Bütün sahələri doldurun |
| "Bu istifadəçi adı artıq mövcuddur" | Dublikat ad | Başqa ad seçin |
| "Yanlış istifadəçi adı və ya şifrə" | Səhv kimlik | Doğru kimlik daxil edin |
| "Bu Tələbə Nömrəsi artıq mövcuddur" | Dublikat nömrə | Başqa nömrə seçin |

## 📁 Əsas Fayllar

| Fayl | Məqsəd |
|------|--------|
| Register.aspx | Qeydiyyat Səhifəsi |
| Login.aspx | Daxıl Ol Səhifəsi |
| Students.aspx | Tələbə İdarəetməsi |
| Logout.aspx | Çıxış Logikası |
| App_Data/users.json | İstifadəçi Məlumatları |
| App_Data/students.json | Tələbə Məlumatları |

## 🔄 Sessiya Müddətləri

- **Session**: Brauzer bağlanana qədər
- **Cookie**: 30 dəqiqə
- **Logout**: Avtomatik temizlənir

## 📊 Nümunə JSON Formatı

### users.json
```json
[
  {
    "Username": "testuser",
    "Password": "password123",
    "Email": "test@example.com"
  }
]
```

### students.json
```json
[
  {
    "Name": "Əhməd",
    "Surname": "Həsənov",
    "StudentNumber": "2023001"
  }
]
```

## 🎯 Səhifə Axını

```
Default.aspx
    ↓
    ├─→ Qeydiyyat → Register.aspx → Login.aspx
    │
    └─→ Daxıl ol → Login.aspx → Students.aspx
                                    ↓
                            (Tələbə Əməliyyatları)
                                    ↓
                                Logout → Login.aspx
```

## 🔧 Təkniyi Təfərrüatlar

- **Framework**: .NET Framework 4.7.2
- **Dil**: C# (Code-Behind)
- **Markup**: ASP.NET WebForms (ASPX)
- **Məlumat**: JSON (System.IO)
- **UI**: HTML + CSS + GridView
- **Sessiya**: In-Process (In-memory)

## ✅ Kontrol Siyahısı

- [x] Qeydiyyat işləyir
- [x] Daxıl olmaq işləyir
- [x] Sessiya işləyir
- [x] Cookie işləyir
- [x] Tələbə əlavə etmə işləyir
- [x] Tələbə redaktə etmə işləyir
- [x] Tələbə silmə işləyir
- [x] Çıxış işləyir
- [x] Doğrulama işləyir
- [x] JSON saxlanması işləyir
- [x] Dil Azerbaijcədir
- [x] Heç bir xəta yoxdur

## 🚀 Hazır istifadəyə!

System hazırdır. Daha bir şey edə bilərsiniz:
1. Tasarımı yaxşılaştırın (CSS)
2. Daha çox validasyon əlavə edin
3. Axtarış funksiyası əlavə edin
4. Sifariş opsiyası əlavə edin

---

**Soruşlarınız varsa, TESTING_GUIDE_AZ.md baxın** ✅
