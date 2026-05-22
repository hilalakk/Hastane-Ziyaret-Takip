CREATE TABLE Hasta
(
    HastaID INT PRIMARY KEY IDENTITY(1,1),
    Ad NVARCHAR(50),
    Soyad NVARCHAR(50),
    TCNo NVARCHAR(11),
    OdaNo NVARCHAR(10)
)

CREATE TABLE Ziyaretci
(
    ZiyaretciID INT PRIMARY KEY IDENTITY(1,1),
    Ad NVARCHAR(50),
    Soyad NVARCHAR(50),
    TCNo NVARCHAR(11)
)

CREATE TABLE Ziyaret
(
    ZiyaretID INT PRIMARY KEY IDENTITY(1,1),
    HastaID INT,
    ZiyaretciID INT,
    GirisSaati DATETIME,
    CikisSaati DATETIME NULL,

    FOREIGN KEY (HastaID)
    REFERENCES Hasta(HastaID),

    FOREIGN KEY (ZiyaretciID)
    REFERENCES Ziyaretci(ZiyaretciID)
)