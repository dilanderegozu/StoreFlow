# 🛒 StoreFlow

> Entity Framework Core ile gerçek dünya senaryoları üzerinden ileri seviye LINQ sorguları

![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![Entity Framework Core](https://img.shields.io/badge/Entity%20Framework%20Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)

---

## 📌 Proje Hakkında

**StoreFlow**, Entity Framework Core'un sunduğu LINQ yeteneklerini gerçekçi bir mağaza senaryosu üzerinden keşfetmek amacıyla geliştirilmiş bir çalışma projesidir.

Bu projede odak noktası yalnızca "nasıl yazılır?" değil, **"hangi durumda hangi sorgu kullanılır?"** sorusuna yanıt bulmaktır. Temel CRUD işlemlerinin ötesine geçilerek veri filtreleme, gruplama, sayfalama ve ilişki yönetimi gibi ileri seviye konular ele alınmıştır.

---

## 🚀 Kullanılan Teknolojiler

| Teknoloji | Açıklama |
|---|---|
| **.NET** | Uygulama çerçevesi |
| **C#** | Programlama dili |
| **Entity Framework Core** | ORM aracı |
| **LINQ** | Veri sorgulama |
| **SQL Server / SQLite** | Veritabanı |

---

## 🔍 Ele Alınan Sorgu Senaryoları

### 1. 🎯 Filtreleme & İlişki Yönetimi
`Where`, `Select`, `Include` kullanılarak veri filtreleme ve ilişkili tabloları (navigation properties) çekme senaryoları.

```csharp
// Örnek: Stoku 10'dan fazla olan ürünleri kategorisiyle birlikte getir
var products = context.Products
    .Where(p => p.Stock > 10)
    .Include(p => p.Category)
    .Select(p => new { p.Name, p.Price, p.Category.CategoryName })
    .ToList();
```

---

### 2. 📊 Gruplama & Analiz
`GroupBy` ile anlamlı raporlama ve veri analizi.

```csharp
// Örnek: Kategoriye göre ürün sayısı ve ortalama fiyat
var report = context.Products
    .GroupBy(p => p.Category.CategoryName)
    .Select(g => new
    {
        Category  = g.Key,
        Count     = g.Count(),
        AvgPrice  = g.Average(p => p.Price)
    })
    .ToList();
```

---

### 3. 🔗 Tablolar Arası İlişki
`Join` ve `GroupJoin` ile ilişkisel sorgu senaryoları.

```csharp
// Örnek: Siparişleri müşteri bilgisiyle birlikte getir
var orders = context.Orders
    .Join(context.Customers,
        o => o.CustomerId,
        c => c.Id,
        (o, c) => new { c.FullName, o.OrderDate, o.TotalAmount })
    .ToList();
```

---

### 4. ✅ Veri Kontrol Metotları
`Any`, `All`, `Count` ile varlık ve doğrulama kontrolleri.

```csharp
// Örnek: Stokta ürün var mı?
bool hasStock = context.Products.Any(p => p.Stock > 0);

// Örnek: Tüm ürünler aktif mi?
bool allActive = context.Products.All(p => p.IsActive);
```

---

### 5. 📄 Sayfalama (Pagination)
`OrderBy`, `Skip`, `Take` ile performanslı sayfalama.

```csharp
// Örnek: 2. sayfa, sayfa başına 10 ürün
int page = 2, pageSize = 10;

var paged = context.Products
    .OrderBy(p => p.Name)
    .Skip((page - 1) * pageSize)
    .Take(pageSize)
    .ToList();
```

---

### 6. 🎯 Tek Kayıt Çekme Stratejileri
`FirstOrDefault`, `SingleOrDefault` — doğru durumda doğru metot.

```csharp
// İlk eşleşen kayıt (yoksa null döner)
var product = context.Products.FirstOrDefault(p => p.Id == id);

// Tek ve benzersiz kayıt beklentisi (birden fazla varsa exception)
var user = context.Users.SingleOrDefault(u => u.Email == email);
```

---

### 7. 🛡️ Boş Veri Yönetimi
`DefaultIfEmpty` ile null ya da boş koleksiyon durumlarının güvenli şekilde yönetimi.

```csharp
// Siparişi olmayan müşterileri de listele (Left Join davranışı)
var result = context.Customers
    .GroupJoin(context.Orders,
        c => c.Id,
        o => o.CustomerId,
        (c, orders) => new { c.FullName, Orders = orders.DefaultIfEmpty() })
    .ToList();
```

---

## 💡 Geliştirme Sürecinde Dikkat Edilen Noktalar

- **Performans:** Gereksiz veri çekmemek için `Select` projeksiyonu ve `AsNoTracking()` kullanımı
- **Okunabilirlik:** Zincirleme sorgular yerine anlamlı ve ayrıştırılmış LINQ yapıları
- **Doğru Metot Seçimi:** `First` vs `Single`, `Join` vs `GroupJoin` gibi nüansların bilinçli kullanımı
- **Gerçekçi Senaryolar:** Yapay örnekler yerine gerçek bir mağaza akışına yakın veri modeli

---

---

## ⚙️ Kurulum & Çalıştırma

### Gereksinimler
- [.NET SDK](https://dotnet.microsoft.com/download) (8.0 veya üzeri)
- SQL Server ya da SQLite

### Adımlar

```bash
# 1. Repoyu klonla
git clone https://github.com/dilanderegozu/StoreFlow.git
cd StoreFlow

# 2. Bağımlılıkları yükle
dotnet restore

# 3. Veritabanını oluştur ve migration uygula
dotnet ef database update

# 4. Projeyi çalıştır
dotnet run
```

---

## 📚 Öğrenilen & Pekiştirilen Kavramlar

| Kavram | Kullanılan Metotlar |
|---|---|
| Filtreleme | `Where`, `Select`, `Include` |
| Gruplama | `GroupBy`, `Having` |
| Join İşlemleri | `Join`, `GroupJoin` |
| Kontrol Metotları | `Any`, `All`, `Count` |
| Sayfalama | `OrderBy`, `Skip`, `Take` |
| Tek Kayıt | `First`, `FirstOrDefault`, `Single`, `SingleOrDefault` |
| Boş Veri | `DefaultIfEmpty` |

---

## 🤝 Katkı

Geri bildirim, öneri ve pull request'lerinizi bekliyorum!  
Bir sorun bulduysanız [Issues](https://github.com/dilanderegozu/StoreFlow/issues) sekmesinden bildirebilirsiniz.

---

## 📄 Lisans

Bu proje [MIT Lisansı](LICENSE) ile lisanslanmıştır.

---

<div align="center">
  <sub>💻 Entity Framework Core öğrenme yolculuğunun bir parçası olarak geliştirildi.</sub>
</div>
