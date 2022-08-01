
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/22/2022 22:14:20
-- Generated from EDMX file: D:\HK3_NH21-22\SEP\Vinasa\Vinasa\Models\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_dbo_NGUOINHANGIAITHUONG_dbo_GIAITHUONG_GiaiThuongId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NGUOINHANGIAITHUONG] DROP CONSTRAINT [FK_dbo_NGUOINHANGIAITHUONG_dbo_GIAITHUONG_GiaiThuongId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_SeminarParticipants_dbo_Seminars_SeminarId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SeminarParticipants] DROP CONSTRAINT [FK_dbo_SeminarParticipants_dbo_Seminars_SeminarId];
GO
IF OBJECT_ID(N'[dbo].[FK_HOIVIEN_KHUVUC]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[HOIVIEN] DROP CONSTRAINT [FK_HOIVIEN_KHUVUC];
GO
IF OBJECT_ID(N'[dbo].[FK_TAIKHOANADMIN_QUYEN]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TAIKHOANADMIN] DROP CONSTRAINT [FK_TAIKHOANADMIN_QUYEN];
GO
IF OBJECT_ID(N'[dbo].[FK_TAIKHOANADMIN_TRANGTHAI]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TAIKHOANADMIN] DROP CONSTRAINT [FK_TAIKHOANADMIN_TRANGTHAI];
GO
IF OBJECT_ID(N'[dbo].[FK_THAMGIAHOINGHIQUOCTE_HOINGHIQUOCTE]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[THAMGIAHOINGHIQUOCTE] DROP CONSTRAINT [FK_THAMGIAHOINGHIQUOCTE_HOINGHIQUOCTE];
GO
IF OBJECT_ID(N'[dbo].[FK_THAMGIAKHOAHOC_KHOAHOC]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[THAMGIAKHOAHOC] DROP CONSTRAINT [FK_THAMGIAKHOAHOC_KHOAHOC];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[__MigrationHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[__MigrationHistory];
GO
IF OBJECT_ID(N'[dbo].[GIAITHUONG]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GIAITHUONG];
GO
IF OBJECT_ID(N'[dbo].[HOINGHIQUOCTE]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HOINGHIQUOCTE];
GO
IF OBJECT_ID(N'[dbo].[HOIVIEN]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HOIVIEN];
GO
IF OBJECT_ID(N'[dbo].[KHOAHOC]', 'U') IS NOT NULL
    DROP TABLE [dbo].[KHOAHOC];
GO
IF OBJECT_ID(N'[dbo].[KHUVUC]', 'U') IS NOT NULL
    DROP TABLE [dbo].[KHUVUC];
GO
IF OBJECT_ID(N'[dbo].[NGUOINHANGIAITHUONG]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NGUOINHANGIAITHUONG];
GO
IF OBJECT_ID(N'[dbo].[Provinces]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Provinces];
GO
IF OBJECT_ID(N'[dbo].[QUYEN]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QUYEN];
GO
IF OBJECT_ID(N'[dbo].[SeminarParticipants]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SeminarParticipants];
GO
IF OBJECT_ID(N'[dbo].[Seminars]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Seminars];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[TAIKHOANADMIN]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TAIKHOANADMIN];
GO
IF OBJECT_ID(N'[dbo].[THAMGIAHOINGHIQUOCTE]', 'U') IS NOT NULL
    DROP TABLE [dbo].[THAMGIAHOINGHIQUOCTE];
GO
IF OBJECT_ID(N'[dbo].[THAMGIAKHOAHOC]', 'U') IS NOT NULL
    DROP TABLE [dbo].[THAMGIAKHOAHOC];
GO
IF OBJECT_ID(N'[dbo].[TRANGTHAI]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TRANGTHAI];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'HOIVIENs'
CREATE TABLE [dbo].[HOIVIENs] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [MaSoThue] varchar(20)  NOT NULL,
    [TenTiengViet] nvarchar(max)  NOT NULL,
    [TenTiengAnh] nvarchar(max)  NOT NULL,
    [TenVietTat] nvarchar(max)  NOT NULL,
    [NgayThanhLap] nvarchar(10)  NOT NULL,
    [Website] nvarchar(max)  NOT NULL,
    [SdtCongTy] varchar(20)  NOT NULL,
    [EmailCongTy] nvarchar(50)  NOT NULL,
    [DiaChiGiaoDich] nvarchar(max)  NOT NULL,
    [DiaChiTrenChungTu] nvarchar(max)  NOT NULL,
    [SoLuongNhanVien] int  NOT NULL,
    [SoLuongLapTrinhVien] int  NOT NULL,
    [ThiTruongNoiDia] nvarchar(max)  NOT NULL,
    [ThiTruongQuocTe] nvarchar(max)  NOT NULL,
    [LinhVucHoatDong] nvarchar(max)  NULL,
    [LanhDao] nvarchar(50)  NOT NULL,
    [ChucDanhLanhDao] nvarchar(50)  NOT NULL,
    [SdtLanhDao] varchar(20)  NOT NULL,
    [EmailLanhDao] nvarchar(50)  NOT NULL,
    [DaiDienMarketing] nvarchar(max)  NOT NULL,
    [ChucNangMarketing] nvarchar(50)  NOT NULL,
    [SdtMarketing] varchar(20)  NOT NULL,
    [EmailMarketing] nvarchar(50)  NOT NULL,
    [DaiDienNhanSu] nvarchar(max)  NOT NULL,
    [ChucDanhNhanSu] nvarchar(50)  NOT NULL,
    [SdtNhanSu] varchar(20)  NOT NULL,
    [EmailNhanSu] nvarchar(50)  NOT NULL,
    [DaiDienKeToan] nvarchar(max)  NOT NULL,
    [ChucDanhKeToan] nvarchar(50)  NOT NULL,
    [SdtKeToan] varchar(20)  NOT NULL,
    [EmailKeToan] nvarchar(50)  NOT NULL,
    [Fanpage] nvarchar(max)  NOT NULL,
    [ThoiGianGiaNhap] nvarchar(10)  NOT NULL,
    [KhuVuc] int  NOT NULL
);
GO

-- Creating table 'KHUVUCs'
CREATE TABLE [dbo].[KHUVUCs] (
    [IDKhuVuc] int IDENTITY(1,1) NOT NULL,
    [TenKhuVuc] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'QUYENs'
CREATE TABLE [dbo].[QUYENs] (
    [IDQuyen] int IDENTITY(1,1) NOT NULL,
    [TenQuyen] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'TAIKHOANADMINs'
CREATE TABLE [dbo].[TAIKHOANADMINs] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Ten] nvarchar(50)  NOT NULL,
    [Email] varchar(100)  NOT NULL,
    [Quyen] int  NOT NULL,
    [TrangThai] int  NOT NULL,
    [Sdt] varchar(20)  NOT NULL,
    [PhongBan] nvarchar(50)  NOT NULL,
    [MatKhau] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'TRANGTHAIs'
CREATE TABLE [dbo].[TRANGTHAIs] (
    [IDTrangThai] int IDENTITY(1,1) NOT NULL,
    [TenTrangThai] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Provinces'
CREATE TABLE [dbo].[Provinces] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NULL
);
GO

-- Creating table 'SeminarParticipants'
CREATE TABLE [dbo].[SeminarParticipants] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SeminarId] int  NULL,
    [Name] nvarchar(max)  NOT NULL,
    [TaxNumber] nvarchar(max)  NOT NULL,
    [Company] nvarchar(max)  NOT NULL,
    [Position] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [PhoneNumber] nvarchar(max)  NOT NULL,
    [ProvinceId] int  NOT NULL,
    [JobTitle] nvarchar(max)  NOT NULL,
    [Operation] nvarchar(max)  NOT NULL,
    [RegistrySeminar] bit  NOT NULL,
    [RegistryBusinessMatching] bit  NOT NULL,
    [RegistryExhibition] bit  NOT NULL,
    [RegistryTicket] bit  NOT NULL,
    [CreatedUtc] datetime  NULL
);
GO

-- Creating table 'Seminars'
CREATE TABLE [dbo].[Seminars] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [OpenDate] datetime  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [CreatedUtc] datetime  NULL
);
GO

-- Creating table 'KHOAHOCs'
CREATE TABLE [dbo].[KHOAHOCs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [TenKhoaDaoTao] nvarchar(max)  NOT NULL,
    [NgayBatDau] datetime  NOT NULL,
    [NgayKetThuc] datetime  NOT NULL,
    [HinhThuc] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'THAMGIAKHOAHOCs'
CREATE TABLE [dbo].[THAMGIAKHOAHOCs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [HoTen] nvarchar(max)  NOT NULL,
    [CongTyToChucCoQuan] nvarchar(max)  NOT NULL,
    [ChucDanh] nvarchar(max)  NOT NULL,
    [Email] varchar(100)  NOT NULL,
    [Sdt] varchar(20)  NOT NULL,
    [SoLuongHocVien] int  NOT NULL,
    [HoiVienVinasa] bit  NOT NULL,
    [IdKhoaHoc] int  NULL
);
GO

-- Creating table 'HOINGHIQUOCTEs'
CREATE TABLE [dbo].[HOINGHIQUOCTEs] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Ten] nvarchar(500)  NOT NULL,
    [DiaDiem] nvarchar(300)  NOT NULL,
    [ThoiGianBatDau] datetime  NOT NULL,
    [ThoiGianKetThuc] datetime  NOT NULL
);
GO

-- Creating table 'THAMGIAHOINGHIQUOCTEs'
CREATE TABLE [dbo].[THAMGIAHOINGHIQUOCTEs] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [TenDonVi] nvarchar(300)  NOT NULL,
    [DonViChungToiLa] nvarchar(100)  NOT NULL,
    [DiaChi] nvarchar(300)  NOT NULL,
    [DienThoai] varchar(10)  NOT NULL,
    [DaiDienLienHe] nvarchar(max)  NOT NULL,
    [ChucVu] nvarchar(50)  NOT NULL,
    [DiDong] varchar(10)  NOT NULL,
    [Email] varchar(50)  NOT NULL,
    [DangKyThamDu] bit  NOT NULL,
    [DangKyPhatBieu] bit  NOT NULL,
    [DangKyGianHangTrienLam] bit  NOT NULL,
    [DangKyBusinessMatchingOnline] bit  NOT NULL,
    [DangKyBusinessMatchingOffline] bit  NOT NULL,
    [DangKyTaiTro] bit  NOT NULL,
    [DangKyQuangCao] bit  NOT NULL,
    [HoiNghiQT_ID] int  NULL
);
GO

-- Creating table 'C__MigrationHistory'
CREATE TABLE [dbo].[C__MigrationHistory] (
    [MigrationId] nvarchar(150)  NOT NULL,
    [ContextKey] nvarchar(300)  NOT NULL,
    [Model] varbinary(max)  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'GIAITHUONGs'
CREATE TABLE [dbo].[GIAITHUONGs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [OpenDate] datetime  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [CreatedUtc] datetime  NULL
);
GO

-- Creating table 'NGUOINHANGIAITHUONGs'
CREATE TABLE [dbo].[NGUOINHANGIAITHUONGs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [GiaiThuongId] int  NULL,
    [MaSoThue] nvarchar(max)  NOT NULL,
    [TenDonVi] nvarchar(max)  NOT NULL,
    [TenNguoiDaiDienPhapLuat] nvarchar(max)  NOT NULL,
    [ChucDanh] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [DiDong] nvarchar(max)  NOT NULL,
    [TenNguoiLienHeVoiBTC] nvarchar(max)  NOT NULL,
    [ChucDanhNguoiLienHe] nvarchar(max)  NOT NULL,
    [EmailNguoiLienHe] nvarchar(max)  NOT NULL,
    [DiDongNguoiLienHe] nvarchar(max)  NOT NULL,
    [ProvinceId] int  NOT NULL,
    [DiaChi] nvarchar(max)  NOT NULL,
    [PhieuDangKy] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'HOIVIENs'
ALTER TABLE [dbo].[HOIVIENs]
ADD CONSTRAINT [PK_HOIVIENs]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [IDKhuVuc] in table 'KHUVUCs'
ALTER TABLE [dbo].[KHUVUCs]
ADD CONSTRAINT [PK_KHUVUCs]
    PRIMARY KEY CLUSTERED ([IDKhuVuc] ASC);
GO

-- Creating primary key on [IDQuyen] in table 'QUYENs'
ALTER TABLE [dbo].[QUYENs]
ADD CONSTRAINT [PK_QUYENs]
    PRIMARY KEY CLUSTERED ([IDQuyen] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [ID] in table 'TAIKHOANADMINs'
ALTER TABLE [dbo].[TAIKHOANADMINs]
ADD CONSTRAINT [PK_TAIKHOANADMINs]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [IDTrangThai] in table 'TRANGTHAIs'
ALTER TABLE [dbo].[TRANGTHAIs]
ADD CONSTRAINT [PK_TRANGTHAIs]
    PRIMARY KEY CLUSTERED ([IDTrangThai] ASC);
GO

-- Creating primary key on [Id] in table 'Provinces'
ALTER TABLE [dbo].[Provinces]
ADD CONSTRAINT [PK_Provinces]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SeminarParticipants'
ALTER TABLE [dbo].[SeminarParticipants]
ADD CONSTRAINT [PK_SeminarParticipants]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Seminars'
ALTER TABLE [dbo].[Seminars]
ADD CONSTRAINT [PK_Seminars]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'KHOAHOCs'
ALTER TABLE [dbo].[KHOAHOCs]
ADD CONSTRAINT [PK_KHOAHOCs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'THAMGIAKHOAHOCs'
ALTER TABLE [dbo].[THAMGIAKHOAHOCs]
ADD CONSTRAINT [PK_THAMGIAKHOAHOCs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [ID] in table 'HOINGHIQUOCTEs'
ALTER TABLE [dbo].[HOINGHIQUOCTEs]
ADD CONSTRAINT [PK_HOINGHIQUOCTEs]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'THAMGIAHOINGHIQUOCTEs'
ALTER TABLE [dbo].[THAMGIAHOINGHIQUOCTEs]
ADD CONSTRAINT [PK_THAMGIAHOINGHIQUOCTEs]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [MigrationId], [ContextKey] in table 'C__MigrationHistory'
ALTER TABLE [dbo].[C__MigrationHistory]
ADD CONSTRAINT [PK_C__MigrationHistory]
    PRIMARY KEY CLUSTERED ([MigrationId], [ContextKey] ASC);
GO

-- Creating primary key on [Id] in table 'GIAITHUONGs'
ALTER TABLE [dbo].[GIAITHUONGs]
ADD CONSTRAINT [PK_GIAITHUONGs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'NGUOINHANGIAITHUONGs'
ALTER TABLE [dbo].[NGUOINHANGIAITHUONGs]
ADD CONSTRAINT [PK_NGUOINHANGIAITHUONGs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [KhuVuc] in table 'HOIVIENs'
ALTER TABLE [dbo].[HOIVIENs]
ADD CONSTRAINT [FK_HOIVIEN_KHUVUC]
    FOREIGN KEY ([KhuVuc])
    REFERENCES [dbo].[KHUVUCs]
        ([IDKhuVuc])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HOIVIEN_KHUVUC'
CREATE INDEX [IX_FK_HOIVIEN_KHUVUC]
ON [dbo].[HOIVIENs]
    ([KhuVuc]);
GO

-- Creating foreign key on [Quyen] in table 'TAIKHOANADMINs'
ALTER TABLE [dbo].[TAIKHOANADMINs]
ADD CONSTRAINT [FK_TAIKHOANADMIN_QUYEN]
    FOREIGN KEY ([Quyen])
    REFERENCES [dbo].[QUYENs]
        ([IDQuyen])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TAIKHOANADMIN_QUYEN'
CREATE INDEX [IX_FK_TAIKHOANADMIN_QUYEN]
ON [dbo].[TAIKHOANADMINs]
    ([Quyen]);
GO

-- Creating foreign key on [TrangThai] in table 'TAIKHOANADMINs'
ALTER TABLE [dbo].[TAIKHOANADMINs]
ADD CONSTRAINT [FK_TAIKHOANADMIN_TRANGTHAI]
    FOREIGN KEY ([TrangThai])
    REFERENCES [dbo].[TRANGTHAIs]
        ([IDTrangThai])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TAIKHOANADMIN_TRANGTHAI'
CREATE INDEX [IX_FK_TAIKHOANADMIN_TRANGTHAI]
ON [dbo].[TAIKHOANADMINs]
    ([TrangThai]);
GO

-- Creating foreign key on [SeminarId] in table 'SeminarParticipants'
ALTER TABLE [dbo].[SeminarParticipants]
ADD CONSTRAINT [FK_dbo_SeminarParticipants_dbo_Seminars_SeminarId]
    FOREIGN KEY ([SeminarId])
    REFERENCES [dbo].[Seminars]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_SeminarParticipants_dbo_Seminars_SeminarId'
CREATE INDEX [IX_FK_dbo_SeminarParticipants_dbo_Seminars_SeminarId]
ON [dbo].[SeminarParticipants]
    ([SeminarId]);
GO

-- Creating foreign key on [IdKhoaHoc] in table 'THAMGIAKHOAHOCs'
ALTER TABLE [dbo].[THAMGIAKHOAHOCs]
ADD CONSTRAINT [FK_THAMGIAKHOAHOC_KHOAHOC]
    FOREIGN KEY ([IdKhoaHoc])
    REFERENCES [dbo].[KHOAHOCs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_THAMGIAKHOAHOC_KHOAHOC'
CREATE INDEX [IX_FK_THAMGIAKHOAHOC_KHOAHOC]
ON [dbo].[THAMGIAKHOAHOCs]
    ([IdKhoaHoc]);
GO

-- Creating foreign key on [HoiNghiQT_ID] in table 'THAMGIAHOINGHIQUOCTEs'
ALTER TABLE [dbo].[THAMGIAHOINGHIQUOCTEs]
ADD CONSTRAINT [FK_THAMGIAHOINGHIQUOCTE_HOINGHIQUOCTE]
    FOREIGN KEY ([HoiNghiQT_ID])
    REFERENCES [dbo].[HOINGHIQUOCTEs]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_THAMGIAHOINGHIQUOCTE_HOINGHIQUOCTE'
CREATE INDEX [IX_FK_THAMGIAHOINGHIQUOCTE_HOINGHIQUOCTE]
ON [dbo].[THAMGIAHOINGHIQUOCTEs]
    ([HoiNghiQT_ID]);
GO

-- Creating foreign key on [GiaiThuongId] in table 'NGUOINHANGIAITHUONGs'
ALTER TABLE [dbo].[NGUOINHANGIAITHUONGs]
ADD CONSTRAINT [FK_dbo_NGUOINHANGIAITHUONG_dbo_GIAITHUONG_GiaiThuongId]
    FOREIGN KEY ([GiaiThuongId])
    REFERENCES [dbo].[GIAITHUONGs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_NGUOINHANGIAITHUONG_dbo_GIAITHUONG_GiaiThuongId'
CREATE INDEX [IX_FK_dbo_NGUOINHANGIAITHUONG_dbo_GIAITHUONG_GiaiThuongId]
ON [dbo].[NGUOINHANGIAITHUONGs]
    ([GiaiThuongId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------