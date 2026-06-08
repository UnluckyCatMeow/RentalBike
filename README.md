# RentalBike

Веб-застосунок для оренди велосипедів, створений на **ASP.NET Core 8** з використанням **Entity Framework Core** та **Identity**. Проєкт підтримує реєстрацію/авторизацію користувачів, ролі (Admin/User), керування каталогом велосипедів та оформлення оренди.

## Основний функціонал

- **Каталог велосипедів**
  - Перегляд списку доступних велосипедів
  - Пошук за моделлю
  - Фільтрація за типом велосипеда
  - Сортування за ціною (зростання/спадання)

- **Оренда велосипеда**
  - Авторизований користувач може оформити оренду
  - Вказання кількості годин, ПІБ, телефону, email
  - Автоматичний розрахунок вартості: `ціна за годину * кількість годин`
  - Зменшення кількості доступних велосипедів, зміна доступності

- **Особистий кабінет користувача**
  - Перегляд власних оренд (**MyRentals**)
  - Відображення інформації про велосипед, дати оренди, статус, суму

- **Адмін-панель (роль Admin)**
  - Керування велосипедами:
    - Перегляд списку
    - Створення нового велосипеда
    - Редагування
    - Видалення
  - Перегляд усіх оренд:
    - Список оренд з користувачами та велосипедами
    - Завершення оренди (зміна статусу на `Completed`, встановлення дати завершення)
  - Керування користувачами:
    - Перегляд списку користувачів
    - Створення нового користувача з роллю **User** через форму (email, пароль, ім’я, прізвище)

## Технології

- **Backend:**
  - ASP.NET Core 8 (MVC + Razor Pages)
  - Entity Framework Core (Code First, Migrations)
  - ASP.NET Core Identity (ApplicationUser з додатковими полями)

- **База даних:**
  - SQL Server (LocalDB)
  - Підключення через `DefaultConnection` у `appsettings.json`:
    - `Server=(localdb)\\MSSQLLocalDB;Database=RentalBikeDb;Trusted_Connection=True;MultipleActiveResultSets=true`

- **Локалізація:**
  - Культура за замовчуванням: `uk-UA`
  - Налаштування в `Program.cs` через `RequestLocalizationOptions`

## Структура проєкту

- **Program.cs**
  - Налаштування служб:
    - MVC (`AddControllersWithViews`)
    - Razor Pages
    - Локалізація (`uk-UA`)
    - DbContext (`ApplicationDbContext`)
    - Identity з кастомним `ApplicationUser` та ролями
  - Маршрут за замовчуванням:
    - `{controller=Bike}/{action=Index}/{id?}`

- **Data**
  - `ApplicationDbContext`:
    - `DbSet<Bike> Bikes`
    - `DbSet<Rental> Rentals`

- **Models**
  - `ApplicationUser`:
    - Наслідує `IdentityUser`
    - Додаткові поля: `FirstName`, `LastName`, `IsBlocked`
  - `Bike`:
    - `Id`, `Type`, `Model`, `Price`, `Color`, `IsAvailable`, `Quantity`, `ImageUrl`
  - `Rental`:
    - `Id`, `BikeId`, `UserId`, `StartDate`, `EndDate`, `TotalPrice`, `Hours`, `FullName`, `Phone`, `Email`, `Status`
    - Навігаційні властивості: `Bike`, `User`
  - `ErrorViewModel` для сторінки помилок

- **Controllers**
  - `BikeController`:
    - `Index(string search, string type, string sort)` — список велосипедів з пошуком/фільтрацією/сортуванням
  - `RentalController`:
    - `[Authorize] Rent(int id)` (GET) — форма оренди
    - `[Authorize] Rent(int id, int hours, string fullName, string phone, string email)` (POST) — створення оренди, зменшення `Quantity`, оновлення `IsAvailable`
    - `[Authorize] MyRentals()` — список оренд поточного користувача
  - `AdminController` (доступний лише для ролі `Admin`):
    - `Bikes()` — список велосипедів
    - `Create()` (GET/POST) — створення велосипеда
    - `Edit(int id)` (GET/POST) — редагування велосипеда
    - `Delete(int id)` (GET/POST) — видалення велосипеда
    - `Rentals()` — список усіх оренд з включенням `Bike` та `User`
    - `CompleteRental(int id)` — завершення оренди (статус `Completed`, `EndDate = DateTime.Now`)
    - `Users()` — список користувачів
    - `CreateUser()` (GET/POST) — створення нового користувача з роллю `User`
