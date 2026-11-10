/*
  個人健康紀錄系統 Personal Health Record System
  資料庫建立腳本（SQL Server / LocalDB）

  內容：
    1. 建立 Health 與 personal 兩張資料表（與 EF Core Migrations 相同結構）
    2. 寫入 __EFMigrationsHistory，讓 EF Core 視為兩個 Migration 已套用
    3. 寫入示範資料（帳號、姓名、電話、地址皆為虛構）

  使用方式：
    在 Visual Studio 的「SQL Server 物件總管」連到 (localdb)\MSSQLLocalDB，
    建立一個空的資料庫後，對該資料庫執行本檔案。
    資料庫名稱要和 appsettings.json 裡 Database= 的名稱相同。
*/

SET NOCOUNT ON;
GO

/* ---------- 1. 資料表 ---------- */

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

CREATE TABLE [Health] (
    [Id] int NOT NULL IDENTITY(1, 1),
    [Account] nvarchar(max) NOT NULL,
    [SystolicPressure] int NOT NULL,
    [DiastolicPressure] int NOT NULL,
    [BloodOxygen] int NOT NULL,
    [HeartRate] int NOT NULL,
    [BodyTemperature] real NOT NULL,
    [FastingBloodSugar] real NOT NULL,
    [PostMealBloodSugar] real NOT NULL,
    [SleepDuration] real NOT NULL,
    [IsExercise] bit NOT NULL,
    [Breakfast] nvarchar(max) NOT NULL,
    [Lunch] nvarchar(max) NOT NULL,
    [Dinner] nvarchar(max) NOT NULL,
    [RecordDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Health] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [personal] (
    [Id] int NOT NULL IDENTITY(1, 1),
    [Account] nvarchar(max) NOT NULL,
    [Password] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [Gender] int NOT NULL,
    [MobilePhone] nvarchar(max) NOT NULL,
    [ContactAddress] nvarchar(max) NOT NULL,
    [EmergencyContactName] nvarchar(max) NOT NULL,
    [EmergencyContactRelationship] nvarchar(max) NOT NULL,
    [EmergencyContactPhone] nvarchar(max) NOT NULL,
    [Height] float NOT NULL,
    [Weight] float NOT NULL,
    [Occupation] nvarchar(max) NOT NULL,
    [MedicalHistory] nvarchar(max) NOT NULL,
    [FamilyHistory] nvarchar(max) NOT NULL,
    [CurrentIllness] nvarchar(max) NOT NULL,
    [SleepPattern] int NOT NULL,
    [BreakfastHabit] int NOT NULL,
    [ExerciseHabit] int NOT NULL,
    [SmokingHabit] int NOT NULL,
    [AlcoholHabit] int NOT NULL,
    [BetelNutHabit] int NOT NULL,
    [HealthPromotionNeeds] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_personal] PRIMARY KEY ([Id])
);
GO

/* ---------- 2. Migration 紀錄 ---------- */

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES
    (N'20250104205425_InitialCreate', N'8.0.11'),
    (N'20250105133442_111', N'8.0.11');
GO

/* ---------- 3. 示範資料（虛構） ----------
   列舉值對照：
     Gender          0 男, 1 女
     SleepPattern    0 每日睡足8小時, 1 不足8小時, 2 時常失眠
     BreakfastHabit  0 每天吃, 1 偶爾, 2 不吃
     ExerciseHabit   0 少於三十分鐘, 1 半小時至一小時, 2 一小時至一小時半, 3 一小時半至兩小時, 4 兩小時以上
     SmokingHabit    0 不吸菸, 1 吸菸
     AlcoholHabit    0 不喝酒, 1 偶爾喝, 2 時常喝
     BetelNutHabit   0 不嚼食檳榔, 1 嚼食檳榔
*/

INSERT INTO [personal] ([Account], [Password], [Name], [DateOfBirth], [Gender], [MobilePhone], [ContactAddress],
    [EmergencyContactName], [EmergencyContactRelationship], [EmergencyContactPhone], [Height], [Weight], [Occupation],
    [MedicalHistory], [FamilyHistory], [CurrentIllness], [SleepPattern], [BreakfastHabit], [ExerciseHabit],
    [SmokingHabit], [AlcoholHabit], [BetelNutHabit], [HealthPromotionNeeds])
VALUES
    (N'DEMO000001', N'demo1234', N'王小明', '1990-05-12', 0, N'0900-000-001', N'花蓮縣示範路 1 號',
     N'王大明', N'父親', N'0900-000-002', 172, 68, N'工程師',
     N'無', N'高血壓', N'無', 1, 0, 1, 0, 1, 0, N'希望改善睡眠'),
    (N'DEMO000002', N'demo1234', N'林小美', '1995-11-03', 1, N'0900-000-003', N'台北市示範街 2 號',
     N'林大美', N'母親', N'0900-000-004', 160, 52, N'護理師',
     N'無', N'糖尿病', N'無', 0, 1, 2, 0, 0, 0, N'控制血糖');
GO

INSERT INTO [Health] ([Account], [SystolicPressure], [DiastolicPressure], [BloodOxygen], [HeartRate], [BodyTemperature],
    [FastingBloodSugar], [PostMealBloodSugar], [SleepDuration], [IsExercise], [Breakfast], [Lunch], [Dinner], [RecordDate])
VALUES
    (N'DEMO000001', 118, 76, 98, 72, 36.5, 92, 125, 7.5, 1, N'三明治', N'便當', N'燙青菜與魚', '2025-01-01T20:00:00'),
    (N'DEMO000001', 132, 86, 97, 88, 36.8, 105, 150, 5.5, 0, N'無', N'牛肉麵', N'炸雞', '2025-01-02T21:30:00'),
    (N'DEMO000001', 121, 79, 96, 75, 37.8, 95, 132, 7.0, 1, N'蛋餅', N'水餃', N'雞湯', '2025-01-03T20:15:00'),
    (N'DEMO000002', 108, 70, 99, 66, 36.4, 88, 118, 8.0, 1, N'燕麥粥', N'沙拉', N'糙米飯', '2025-01-01T19:45:00'),
    (N'DEMO000002', 112, 72, 98, 70, 36.6, 101, 145, 6.5, 0, N'吐司', N'義大利麵', N'無', '2025-01-02T22:00:00');
GO
