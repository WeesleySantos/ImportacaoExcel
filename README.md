# ImportacaoExcel

Passo a passo

Primeiro - Rodar script da tabela no banco de dados:


Script Banco - USE [master]
GO
/****** Object:  Database [DadosPlanilha]    Script Date: 13/10/2024 18:43:45 ******/
CREATE DATABASE [DadosPlanilha]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DadosPlanilha', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\DadosPlanilha.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DadosPlanilha_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\DadosPlanilha_log.ldf' , SIZE = 991232KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DadosPlanilha] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DadosPlanilha].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DadosPlanilha] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DadosPlanilha] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DadosPlanilha] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DadosPlanilha] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DadosPlanilha] SET ARITHABORT OFF 
GO
ALTER DATABASE [DadosPlanilha] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DadosPlanilha] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DadosPlanilha] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DadosPlanilha] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DadosPlanilha] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DadosPlanilha] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DadosPlanilha] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DadosPlanilha] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DadosPlanilha] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DadosPlanilha] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DadosPlanilha] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DadosPlanilha] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DadosPlanilha] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DadosPlanilha] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DadosPlanilha] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DadosPlanilha] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DadosPlanilha] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DadosPlanilha] SET RECOVERY FULL 
GO
ALTER DATABASE [DadosPlanilha] SET  MULTI_USER 
GO
ALTER DATABASE [DadosPlanilha] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DadosPlanilha] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DadosPlanilha] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DadosPlanilha] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DadosPlanilha] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DadosPlanilha] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DadosPlanilha', N'ON'
GO
ALTER DATABASE [DadosPlanilha] SET QUERY_STORE = ON
GO
ALTER DATABASE [DadosPlanilha] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DadosPlanilha]
GO
/****** Object:  Table [dbo].[Planilha]    Script Date: 13/10/2024 18:43:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Planilha](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CodigoCliente] [int] NULL,
	[CategoriaProduto] [varchar](100) NULL,
	[SkuProduto] [varchar](100) NULL,
	[Data] [datetime] NULL,
	[Quantidade] [int] NULL,
	[ValorFaturamento] [decimal](18, 0) NULL
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [DadosPlanilha] SET  READ_WRITE 
GO
------------------------------------------------------------------------------------------------------------------------------------------------------------
Segundo - no Projeto API em appsetings -  colocar o caminho do seu banco.
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=PC-GAMER;Database=DADOSPLANILHA;User Id=sa;Password=admin123;TrustServerCertificate=True;"
  }, 
-------------------------------------------------------------------------------------------------------------------------------------------------------------
Importação de Planilhia:
para importar a planilha na tela inicial, basta clicar em escolher arquivo, após escolher o arquivo, clicar em Upload.. 
Obs: Arquivo que contém um milão de linhas demora certa de 3 minutos para inserir os dados no banco e mostrar na tela.
