Use GestorPedidosEmpresariales_Proyecto2

CREATE TABLE [Cliente] (
  [id_cliente] INT IDENTITY(1,1) PRIMARY KEY,
  [nombre_compania] nvarchar(255),
  [nombre_contacto] nvarchar(255),
  [apellido_contacto] nvarchar(255),
  [puesto_contacto] nvarchar(255),
  [direccion] nvarchar(255),
  [ciudad] nvarchar(255),
  [provincia] nvarchar(255),
  [codigo_postal] nvarchar(255),
  [pais] nvarchar(255),
  [telefono] nvarchar(255),
  [eliminado] bit default 0
)
GO

CREATE TABLE [Empleado] (
  [id_empleado] INT IDENTITY(1,1) PRIMARY KEY,
  [nombre_empleado] nvarchar(255),
  [apellidos_empleado] nvarchar(255),
  [puesto] nvarchar(255),
  [extension] nvarchar(255),
  [telefono_trabajo] nvarchar(255),
  [id_departamento] int,
  [id_rol] int,
  [eliminado] bit default 0
)
GO

CREATE TABLE [Departamento] (
  [id_departamento] INT IDENTITY(1,1) PRIMARY KEY,
  [nombre_departamento] nvarchar(255),
  [eliminado] bit default 0
)
GO

CREATE TABLE [Rol] (
  [id_rol] INT IDENTITY(1,1) PRIMARY KEY,
  [nombre_rol] nvarchar(255),
  [eliminado] bit default 0
)
GO

CREATE TABLE [Orden] (
  [id_orden] INT IDENTITY(1,1) PRIMARY KEY,
  [id_cliente] int,
  [id_empleado] int,
  [fecha_orden] datetime,
  [direccion_viaje] nvarchar(255),
  [ciudad_viaje] nvarchar(255),
  [provincia_viaje] nvarchar(255),
  [pais_viaje] nvarchar(255),
  [telefono_viaje] nvarchar(255),
  [eliminado] bit default 0
)
GO

CREATE TABLE [DetalleOrden] (
  [id_orden] INT ,
  [id_variante] int,
  [cantidad] int,
  [precio_linea] decimal,
  [eliminado] bit default 0,
  PRIMARY KEY ([id_orden], [id_variante])
)
GO

CREATE TABLE [Categoria] (
  [id_categoria] INT IDENTITY(1,1) PRIMARY KEY,
  [descripcion] nvarchar(255),
  [eliminado] bit default 0
)
GO

CREATE TABLE [Pagos] (
  [id_pago] INT IDENTITY(1,1) PRIMARY KEY,
  [id_orden] int,
  [cantidad_pago] decimal,
  [fecha_pago] datetime,
  [num_tarjetaCredito] nvarchar(255),
  [nom_usuario_tarjeta] nvarchar(255),
  [id_metodoPago] int,
  [eliminado] bit default 0
)
GO

CREATE TABLE [MetodoPago] (
  [id_metodo_pago] INT IDENTITY(1,1) PRIMARY KEY,
  [metodo_pago] nvarchar(255),
  [tarjeta_credito] nvarchar(255),
  [eliminado] bit default 0
)
GO

CREATE TABLE [ProductoBase] (
  [id_producto_base] INT IDENTITY(1,1) PRIMARY KEY,
  [nombre_producto] nvarchar(255),
  [id_categoria] int,
  [eliminado] bit default 0
)
GO

CREATE TABLE [VarianteProducto] (
  [id_variante] INT IDENTITY(1,1) PRIMARY KEY,
  [id_producto_base] int,
  [talla] nvarchar(255),
  [descripcion] nvarchar(255),
  [precio] decimal,
  [cantidad_existencias] decimal,
  [punto_reorden] int,
  [eliminado] bit default 0
)
GO

ALTER TABLE [Orden] ADD FOREIGN KEY ([id_cliente]) REFERENCES [Cliente] ([id_cliente])
GO

ALTER TABLE [Orden] ADD FOREIGN KEY ([id_empleado]) REFERENCES [Empleado] ([id_empleado])
GO

ALTER TABLE [Empleado] ADD FOREIGN KEY ([id_departamento]) REFERENCES [Departamento] ([id_departamento])
GO

ALTER TABLE [Empleado] ADD FOREIGN KEY ([id_rol]) REFERENCES [Rol] ([id_rol])
GO

ALTER TABLE [DetalleOrden] ADD FOREIGN KEY ([id_orden]) REFERENCES [Orden] ([id_orden])
GO

ALTER TABLE [Pagos] ADD FOREIGN KEY ([id_orden]) REFERENCES [Orden] ([id_orden])
GO

ALTER TABLE [Pagos] ADD FOREIGN KEY ([id_metodoPago]) REFERENCES [MetodoPago] ([id_metodo_pago])
GO

ALTER TABLE [VarianteProducto] ADD FOREIGN KEY ([id_producto_base]) REFERENCES [ProductoBase] ([id_producto_base])
GO

ALTER TABLE [DetalleOrden] ADD FOREIGN KEY ([id_variante]) REFERENCES [VarianteProducto] ([id_variante])
GO

ALTER TABLE [ProductoBase] ADD FOREIGN KEY ([id_categoria]) REFERENCES [Categoria] ([id_categoria])
GO


--DROP TABLE IF EXISTS [DetalleOrden];
--DROP TABLE IF EXISTS [Pagos];
--DROP TABLE IF EXISTS [Orden];
--DROP TABLE IF EXISTS [VarianteProducto];
--DROP TABLE IF EXISTS [Producto];
--DROP TABLE IF EXISTS [ProductoBase];
--DROP TABLE IF EXISTS [MetodoPago];
--DROP TABLE IF EXISTS [Categoria];
--DROP TABLE IF EXISTS [Empleado];
--DROP TABLE IF EXISTS [Cliente];
--DROP TABLE IF EXISTS [Departamento];
--DROP TABLE IF EXISTS [Rol];