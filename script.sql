CREATE DATABASE [GestorPedidosEmpresariales_Proyecto2]

USE [GestorPedidosEmpresariales_Proyecto2]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 6/2 7/2025 12:09:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[id_categoria] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nvarchar](255) NULL,
	[eliminado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_categoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 6/27/2025 12:09:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[id_cliente] [int] IDENTITY(1,1) NOT NULL,
	[nombre_compania] [nvarchar](255) NULL,
	[nombre_contacto] [nvarchar](255) NULL,
	[apellido_contacto] [nvarchar](255) NULL,
	[puesto_contacto] [nvarchar](255) NULL,
	[direccion] [nvarchar](255) NULL,
	[ciudad] [nvarchar](255) NULL,
	[provincia] [nvarchar](255) NULL,
	[codigo_postal] [nvarchar](255) NULL,
	[pais] [nvarchar](255) NULL,
	[telefono] [nvarchar](255) NULL,
	[eliminado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_cliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departamento]    Script Date: 6/27/2025 12:09:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departamento](
	[id_departamento] [int] IDENTITY(1,1) NOT NULL,
	[nombre_departamento] [nvarchar](255) NULL,
	[eliminado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_departamento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleOrden]    Script Date: 6/27/2025 12:09:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleOrden](
	[id_orden] [int] NOT NULL,
	[id_variante] [int] NOT NULL,
	[cantidad] [int] NULL,
	[precio_linea] [decimal](18, 0) NULL,
	[eliminado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_orden] ASC,
	[id_variante] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empleado]    Script Date: 6/27/2025 12:09:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empleado](
	[id_empleado] [int] IDENTITY(1,1) NOT NULL,
	[nombre_empleado] [nvarchar](255) NULL,
	[apellidos_empleado] [nvarchar](255) NULL,
	[puesto] [nvarchar](255) NULL,
	[extension] [nvarchar](255) NULL,
	[telefono_trabajo] [nvarchar](255) NULL,
	[id_departamento] [int] NULL,
	[id_rol] [int] NULL,
	[eliminado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_empleado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MetodoPago]    Script Date: 6/27/2025 12:09:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MetodoPago](
	[id_metodo_pago] [int] IDENTITY(1,1) NOT NULL,
	[metodo_pago] [nvarchar](255) NULL,
	[tarjeta_credito] [nvarchar](255) NULL,
	[eliminado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_metodo_pago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orden]    Script Date: 6/27/2025 12:09:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orden](
	[id_orden] [int] IDENTITY(1,1) NOT NULL,
	[id_cliente] [int] NULL,
	[id_empleado] [int] NULL,
	[fecha_orden] [datetime] NULL,
	[direccion_viaje] [nvarchar](255) NULL,
	[ciudad_viaje] [nvarchar](255) NULL,
	[provincia_viaje] [nvarchar](255) NULL,
	[pais_viaje] [nvarchar](255) NULL,
	[telefono_viaje] [nvarchar](255) NULL,
	[eliminado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_orden] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pago]    Script Date: 6/27/2025 12:09:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pago](
	[id_pago] [int] IDENTITY(1,1) NOT NULL,
	[id_orden] [int] NULL,
	[cantidad_pago] [decimal](18, 0) NULL,
	[fecha_pago] [datetime] NULL,
	[num_tarjetaCredito] [nvarchar](255) NULL,
	[nom_usuario_tarjeta] [nvarchar](255) NULL,
	[id_metodoPago] [int] NULL,
	[eliminado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_pago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductoBase]    Script Date: 6/27/2025 12:09:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductoBase](
	[id_producto_base] [int] IDENTITY(1,1) NOT NULL,
	[nombre_producto] [nvarchar](255) NULL,
	[id_categoria] [int] NULL,
	[eliminado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_producto_base] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 6/27/2025 12:09:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rol](
	[id_rol] [int] IDENTITY(1,1) NOT NULL,
	[nombre_rol] [nvarchar](255) NULL,
	[eliminado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_rol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 6/27/2025 12:09:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[id_empleado] [int] NULL,
	[correo] [nchar](100) NULL,
	[contrasenna] [nchar](100) NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VarianteProducto]    Script Date: 6/27/2025 12:09:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VarianteProducto](
	[id_variante] [int] IDENTITY(1,1) NOT NULL,
	[id_producto_base] [int] NULL,
	[talla] [nvarchar](255) NULL,
	[descripcion] [nvarchar](255) NULL,
	[precio] [decimal](18, 0) NULL,
	[cantidad_existencias] [decimal](18, 0) NULL,
	[punto_reorden] [int] NULL,
	[eliminado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_variante] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Categoria] ADD  DEFAULT ((0)) FOR [eliminado]
GO
ALTER TABLE [dbo].[Cliente] ADD  DEFAULT ((0)) FOR [eliminado]
GO
ALTER TABLE [dbo].[Departamento] ADD  DEFAULT ((0)) FOR [eliminado]
GO
ALTER TABLE [dbo].[DetalleOrden] ADD  DEFAULT ((0)) FOR [eliminado]
GO
ALTER TABLE [dbo].[Empleado] ADD  DEFAULT ((0)) FOR [eliminado]
GO
ALTER TABLE [dbo].[MetodoPago] ADD  DEFAULT ((0)) FOR [eliminado]
GO
ALTER TABLE [dbo].[Orden] ADD  DEFAULT ((0)) FOR [eliminado]
GO
ALTER TABLE [dbo].[Pago] ADD  DEFAULT ((0)) FOR [eliminado]
GO
ALTER TABLE [dbo].[ProductoBase] ADD  DEFAULT ((0)) FOR [eliminado]
GO
ALTER TABLE [dbo].[Rol] ADD  DEFAULT ((0)) FOR [eliminado]
GO
ALTER TABLE [dbo].[VarianteProducto] ADD  DEFAULT ((0)) FOR [eliminado]
GO
ALTER TABLE [dbo].[DetalleOrden]  WITH CHECK ADD FOREIGN KEY([id_orden])
REFERENCES [dbo].[Orden] ([id_orden])
GO
ALTER TABLE [dbo].[DetalleOrden]  WITH CHECK ADD FOREIGN KEY([id_variante])
REFERENCES [dbo].[VarianteProducto] ([id_variante])
GO
ALTER TABLE [dbo].[Empleado]  WITH CHECK ADD FOREIGN KEY([id_departamento])
REFERENCES [dbo].[Departamento] ([id_departamento])
GO
ALTER TABLE [dbo].[Empleado]  WITH CHECK ADD FOREIGN KEY([id_rol])
REFERENCES [dbo].[Rol] ([id_rol])
GO
ALTER TABLE [dbo].[Orden]  WITH CHECK ADD FOREIGN KEY([id_cliente])
REFERENCES [dbo].[Cliente] ([id_cliente])
GO
ALTER TABLE [dbo].[Orden]  WITH CHECK ADD FOREIGN KEY([id_empleado])
REFERENCES [dbo].[Empleado] ([id_empleado])
GO
ALTER TABLE [dbo].[Pago]  WITH CHECK ADD FOREIGN KEY([id_metodoPago])
REFERENCES [dbo].[MetodoPago] ([id_metodo_pago])
GO
ALTER TABLE [dbo].[Pago]  WITH CHECK ADD FOREIGN KEY([id_orden])
REFERENCES [dbo].[Orden] ([id_orden])
GO
ALTER TABLE [dbo].[ProductoBase]  WITH CHECK ADD FOREIGN KEY([id_categoria])
REFERENCES [dbo].[Categoria] ([id_categoria])
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Empleado] FOREIGN KEY([id_empleado])
REFERENCES [dbo].[Empleado] ([id_empleado])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Empleado]
GO
ALTER TABLE [dbo].[VarianteProducto]  WITH CHECK ADD FOREIGN KEY([id_producto_base])
REFERENCES [dbo].[ProductoBase] ([id_producto_base])
GO
