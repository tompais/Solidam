USE [master]
GO
/****** Object:  Database [TP-20192C]    Script Date: 9/24/2019 9:23:33 PM ******/
CREATE DATABASE [TP-20192C]
ALTER DATABASE [TP-20192C] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TP-20192C].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TP-20192C] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TP-20192C] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TP-20192C] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TP-20192C] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TP-20192C] SET ARITHABORT OFF 
GO
ALTER DATABASE [TP-20192C] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TP-20192C] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TP-20192C] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TP-20192C] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TP-20192C] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TP-20192C] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TP-20192C] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TP-20192C] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TP-20192C] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TP-20192C] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TP-20192C] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TP-20192C] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TP-20192C] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TP-20192C] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TP-20192C] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TP-20192C] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TP-20192C] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TP-20192C] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TP-20192C] SET  MULTI_USER 
GO
ALTER DATABASE [TP-20192C] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TP-20192C] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TP-20192C] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TP-20192C] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [TP-20192C] SET DELAYED_DURABILITY = DISABLED 
GO
USE [TP-20192C]
GO
/****** Object:  Table [dbo].[Denuncia]    Script Date: 9/24/2019 9:23:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Denuncia](
	[IdDenuncia] [int] IDENTITY(1,1) NOT NULL,
	[IdPropuesta] [int] NOT NULL,
	[IdMotivo] [int] NOT NULL,
	[Comentarios] [nvarchar](300) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[Estado] [int] NOT NULL,
 CONSTRAINT [PK_Denuncia] PRIMARY KEY CLUSTERED 
(
	[IdDenuncia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DonacionHorasTrabajo]    Script Date: 9/24/2019 9:23:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonacionHorasTrabajo](
	[IdDonacionHorasTrabajo] [int] IDENTITY(1,1) NOT NULL,
	[IdPropuestaDonacionHorasTrabajo] [int] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
 CONSTRAINT [PK_DonacionHorasTrabajo] PRIMARY KEY CLUSTERED 
(
	[IdDonacionHorasTrabajo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DonacionInsumo]    Script Date: 9/24/2019 9:23:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonacionInsumo](
	[IdDonacionInsumo] [int] IDENTITY(1,1) NOT NULL,
	[IdPropuestaDonacionInsumo] [int] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
 CONSTRAINT [PK_DonacionInsumo] PRIMARY KEY CLUSTERED 
(
	[IdDonacionInsumo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DonacionMonetaria]    Script Date: 9/24/2019 9:23:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonacionMonetaria](
	[IdDonacionMonetaria] [int] IDENTITY(1,1) NOT NULL,
	[IdPropuestaDonacionMonetaria] [int] NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[Dinero] [decimal](18, 2) NOT NULL,
	[ArchivoTransferencia] [nvarchar](200) NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
 CONSTRAINT [PK_DonacionMonetaria] PRIMARY KEY CLUSTERED 
(
	[IdDonacionMonetaria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Propuesta]    Script Date: 9/24/2019 9:23:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Propuesta](
	[IdPropuesta] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Descripcion] [text] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[FechaFin] [datetime] NOT NULL,
	[TelefonoContacto] [nvarchar](30) NOT NULL,
	[TipoDonacion] [int] NOT NULL,
	[Foto] [nvarchar](100) NOT NULL,
	[IdUsuarioCreador] [int] NOT NULL,
	[Estado] [int] NOT NULL,
	[Valoracion] [decimal](18, 0) NULL,
 CONSTRAINT [PK_Propuesta] PRIMARY KEY CLUSTERED 
(
	[IdPropuesta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PropuestaDonacionHorasTrabajo]    Script Date: 9/24/2019 9:23:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropuestaDonacionHorasTrabajo](
	[IdPropuestaDonacionHorasTrabajo] [int] IDENTITY(1,1) NOT NULL,
	[IdPropuesta] [int] NOT NULL,
	[CantidadHoras] [int] NOT NULL,
	[Profesion] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PropuestaDonacionHorasTrabajo] PRIMARY KEY CLUSTERED 
(
	[IdPropuestaDonacionHorasTrabajo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PropuestaDonacionInsumo]    Script Date: 9/24/2019 9:23:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropuestaDonacionInsumo](
	[IdPropuestaDonacionInsumo] [int] IDENTITY(1,1) NOT NULL,
	[IdPropuesta] [int] NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Cantidad] [int] NOT NULL,
 CONSTRAINT [PK_PropuestaDonacionInsumo] PRIMARY KEY CLUSTERED 
(
	[IdPropuestaDonacionInsumo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PropuestaDonacionMonetaria]    Script Date: 9/24/2019 9:23:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropuestaDonacionMonetaria](
	[IdPropuestaDonacionMonetaria] [int] IDENTITY(1,1) NOT NULL,
	[IdPropuesta] [int] NOT NULL,
	[Dinero] [decimal](18, 2) NOT NULL,
	[CBU] [nvarchar](80) NOT NULL,
 CONSTRAINT [PK_PropuestaDonacionMonetaria] PRIMARY KEY CLUSTERED 
(
	[IdPropuestaDonacionMonetaria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PropuestaReferencia]    Script Date: 9/24/2019 9:23:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropuestaReferencia](
	[IdReferencia] [int] IDENTITY(1,1) NOT NULL,
	[IdPropuesta] [int] NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Telefono] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_PropuestaReferencia] PRIMARY KEY CLUSTERED 
(
	[IdReferencia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PropuestaValoracion]    Script Date: 9/24/2019 9:23:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropuestaValoracion](
	[IdValoracion] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[IdPropuesta] [int] NOT NULL,
	[Valoracion] [bit] NOT NULL,
 CONSTRAINT [PK_PropuestaValoracion] PRIMARY KEY CLUSTERED 
(
	[IdValoracion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 9/24/2019 9:23:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Apellido] [nvarchar](50) NULL,
	[FechaNacimiento] [datetime] NOT NULL,
	[UserName] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](20) NOT NULL,
	[Foto] [nvarchar](100) NULL,
	[TipoUsuario] [int] NOT NULL,
	[FechaCracion] [datetime] NOT NULL,
	[Activo] [bit] NOT NULL,
	[Token] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].MotivoDenuncia(
IdMotivoDenuncia int not null,
Descripcion varchar(30) not null,
CONSTRAINT PK_MotivoDenuncia PRIMARY KEY(IdMotivoDenuncia)
)
GO

ALTER TABLE [dbo].[Propuesta] ADD  DEFAULT ((0)) FOR [Valoracion]
GO
ALTER TABLE [dbo].[Denuncia]  WITH CHECK ADD  CONSTRAINT [FK_Denuncia_Propuesta] FOREIGN KEY([IdPropuesta])
REFERENCES [dbo].[Propuesta] ([IdPropuesta])
GO
ALTER TABLE [dbo].[Denuncia] CHECK CONSTRAINT [FK_Denuncia_Propuesta]
GO
ALTER TABLE [dbo].[Denuncia]  WITH CHECK ADD  CONSTRAINT [FK_Denuncia_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[Denuncia] CHECK CONSTRAINT [FK_Denuncia_Usuario]
GO
ALTER TABLE [dbo].[DonacionHorasTrabajo]  WITH CHECK ADD  CONSTRAINT [FK_DonacionHorasTrabajo_PropuestaDonacionHorasTrabajo] FOREIGN KEY([IdPropuestaDonacionHorasTrabajo])
REFERENCES [dbo].[PropuestaDonacionHorasTrabajo] ([IdPropuestaDonacionHorasTrabajo])
GO
ALTER TABLE [dbo].[DonacionHorasTrabajo] CHECK CONSTRAINT [FK_DonacionHorasTrabajo_PropuestaDonacionHorasTrabajo]
GO
ALTER TABLE [dbo].[DonacionHorasTrabajo]  WITH CHECK ADD  CONSTRAINT [FK_DonacionHorasTrabajo_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[DonacionHorasTrabajo] CHECK CONSTRAINT [FK_DonacionHorasTrabajo_Usuario]
GO
ALTER TABLE [dbo].[DonacionInsumo]  WITH CHECK ADD  CONSTRAINT [FK_DonacionInsumo_PropuestaDonacionInsumo] FOREIGN KEY([IdPropuestaDonacionInsumo])
REFERENCES [dbo].[PropuestaDonacionInsumo] ([IdPropuestaDonacionInsumo])
GO
ALTER TABLE [dbo].[DonacionInsumo] CHECK CONSTRAINT [FK_DonacionInsumo_PropuestaDonacionInsumo]
GO
ALTER TABLE [dbo].[DonacionInsumo]  WITH CHECK ADD  CONSTRAINT [FK_DonacionInsumo_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[DonacionInsumo] CHECK CONSTRAINT [FK_DonacionInsumo_Usuario]
GO
ALTER TABLE [dbo].[DonacionMonetaria]  WITH CHECK ADD  CONSTRAINT [FK_DonacionMonetaria_PropuestaDonacionMonetaria] FOREIGN KEY([IdPropuestaDonacionMonetaria])
REFERENCES [dbo].[PropuestaDonacionMonetaria] ([IdPropuestaDonacionMonetaria])
GO
ALTER TABLE [dbo].[DonacionMonetaria] CHECK CONSTRAINT [FK_DonacionMonetaria_PropuestaDonacionMonetaria]
GO
ALTER TABLE [dbo].[DonacionMonetaria]  WITH CHECK ADD  CONSTRAINT [FK_DonacionMonetaria_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[DonacionMonetaria] CHECK CONSTRAINT [FK_DonacionMonetaria_Usuario]
GO
ALTER TABLE [dbo].[Propuesta]  WITH CHECK ADD  CONSTRAINT [FK_Propuesta_Usuario] FOREIGN KEY([IdUsuarioCreador])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[Propuesta] CHECK CONSTRAINT [FK_Propuesta_Usuario]
GO
ALTER TABLE [dbo].[PropuestaDonacionHorasTrabajo]  WITH CHECK ADD  CONSTRAINT [FK_PropuestaDonacionHorasTrabajo_Propuesta] FOREIGN KEY([IdPropuesta])
REFERENCES [dbo].[Propuesta] ([IdPropuesta])
GO
ALTER TABLE [dbo].[PropuestaDonacionHorasTrabajo] CHECK CONSTRAINT [FK_PropuestaDonacionHorasTrabajo_Propuesta]
GO
ALTER TABLE [dbo].[PropuestaDonacionInsumo]  WITH CHECK ADD  CONSTRAINT [FK_PropuestaDonacionInsumo_Propuesta] FOREIGN KEY([IdPropuesta])
REFERENCES [dbo].[Propuesta] ([IdPropuesta])
GO
ALTER TABLE [dbo].[PropuestaDonacionInsumo] CHECK CONSTRAINT [FK_PropuestaDonacionInsumo_Propuesta]
GO
ALTER TABLE [dbo].[PropuestaDonacionMonetaria]  WITH CHECK ADD  CONSTRAINT [FK_PropuestaDonacionMonetaria_Propuesta] FOREIGN KEY([IdPropuesta])
REFERENCES [dbo].[Propuesta] ([IdPropuesta])
GO
ALTER TABLE [dbo].[PropuestaDonacionMonetaria] CHECK CONSTRAINT [FK_PropuestaDonacionMonetaria_Propuesta]
GO
ALTER TABLE [dbo].[PropuestaReferencia]  WITH CHECK ADD  CONSTRAINT [FK_PropuestaReferencia_Propuesta] FOREIGN KEY([IdPropuesta])
REFERENCES [dbo].[Propuesta] ([IdPropuesta])
GO
ALTER TABLE [dbo].[PropuestaReferencia] CHECK CONSTRAINT [FK_PropuestaReferencia_Propuesta]
GO
ALTER TABLE [dbo].[PropuestaValoracion]  WITH CHECK ADD  CONSTRAINT [FK_PropuestaValoracion_Propuesta] FOREIGN KEY([IdPropuesta])
REFERENCES [dbo].[Propuesta] ([IdPropuesta])
GO
ALTER TABLE [dbo].[PropuestaValoracion] CHECK CONSTRAINT [FK_PropuestaValoracion_Propuesta]
GO
ALTER TABLE [dbo].[PropuestaValoracion]  WITH CHECK ADD  CONSTRAINT [FK_PropuestaValoracion_Usuario] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[PropuestaValoracion] CHECK CONSTRAINT [FK_PropuestaValoracion_Usuario]
GO
USE [master]
GO
ALTER DATABASE [TP-20192C] SET  READ_WRITE 
GO
