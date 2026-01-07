create database  ;
use DbCrudBlazor;
create table Departamento(
	IdDepartamento int primary key identity(1,1),
	Nombre varchar(50) not null
)
create table Empleado(
	IdEmpleado int primary key identity(1,1),
	NombreCompleto varchar(50) not null,
	IdDepartamento int references Departamento(IdDepartamento) not null,
	Sueldo int not null,
	FechaContrato date not null
)
insert into Departamento(Nombre) values
('Administración'),('Marketing'),('Ventas'),('Comercio')

insert into Empleado(NombreCompleto,IdDepartamento,Sueldo,FechaContrato) values
('Tomas Cienega',1,1500,getdate())
select * from Departamento
select * from Empleado