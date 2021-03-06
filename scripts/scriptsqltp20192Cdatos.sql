USE [TP-20192C];
INSERT [dbo].[MotivoDenuncia] ([IdMotivoDenuncia], [Descripcion]) VALUES (1, N'Fraude')
GO
INSERT [dbo].[MotivoDenuncia] ([IdMotivoDenuncia], [Descripcion]) VALUES (2, N'Indebida')
GO
INSERT [dbo].[MotivoDenuncia] ([IdMotivoDenuncia], [Descripcion]) VALUES (3, N'Violacion de derechos')
GO
INSERT [dbo].[MotivoDenuncia] ([IdMotivoDenuncia], [Descripcion]) VALUES (4, N'Contiene informacion politica')
GO
INSERT INTO [dbo].[Usuarios]
           ([Nombre]
           ,[Apellido]
           ,[FechaNacimiento]
           ,[UserName]
           ,[Email]
           ,[Password]
           ,[Foto]
           ,[TipoUsuario]
           ,[FechaCracion]
           ,[Activo]
           ,[Token])
     VALUES
           ('MARCOS'
           ,'PEREZ'
           ,'01/02/1978'
           ,'admin'
           ,'admin@test.com'
           ,'07b61d74beda5fee4b834db3c966f0a635a33953' -- Admin2019 --
           ,''
           ,1
           ,getdate()
           ,1
           ,'19E41C31E74A4526')
GO

/****** Inserto datos en usuario y propuestas  ******/
INSERT INTO Usuarios values ('Martin','Gallego','19800816','MARTIN.GALLEGO','mgallego@gmail.com','a8dfb6a119f1577637876c472c8922c8190fd923',null,2,getdate(),1,'ESTEESMITOKEN1'), -- Pass Mgallego1234
('Juan','Alberto','19800817','JUAN.ALBERTO','jalberto@gmail.com','7dc43d4f66157297aa95baf8b3a3fae72178615e',null,2,'20191018',1,'ESTEESMITOKEN2'), -- Pass Jalberto1234
('Ezequiel','Rojas','19800818','EZEQUIEL.ROJAS','erojas@gmail.com','99741c1697426292ad19df50ebbc28b837449f90',null,2,'20191018',1,'ESTEESMITOKEN3'), -- Pass Erojas1234
('Esteban','Gonzalez','19800819','ESTEBAN.GONZALEZ','egonzalez@gmail.com','b1fd4792bcdfddab8f3102c4dd7924eddbf43574',null,2,'20191018',1,'ESTEESMITOKEN4') -- Pass Egonzalez1234
GO

select * from Propuestas
INSERT INTO Propuestas values('Construir refugio de Animales',
't is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using Content here, content here, making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text',
'20191017','20191224','1112333333',1,'RefugioAnimales.jpg',2,0,8)
GO

INSERT INTO PropuestasDonacionesMonetarias values (1,300000,'552543245432')
GO

INSERT INTO Propuestas values('Juntos Somos Bosque',
't is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using Content here, content here, making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text',
'20191017','20191224','1112312121',1,'JuntosSomosBosque.jpg',1,0,6)
GO

INSERT INTO PropuestasDonacionesMonetarias values (2,34575,'132132123321')
GO

INSERT INTO Propuestas values('Perchero Solidario',
't is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using Content here, content here, making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text',
'20191017','20191224','112457545',2,'PercheroSolidario.jpg',3,0,7)
GO

INSERT INTO PropuestasDonacionesInsumos values (3,'Ropas',20)
GO

INSERT INTO Propuestas values('Reforma del Centro de Salud 98',
't is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using Content here, content here, making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text',
'20191017','20191224','1112345678',3,'ReformaCentroDeSalud.jpg',4,0,9)
GO

INSERT INTO PropuestasDonacionesHorasTrabajo values(4,36,'Carpintero')
GO

INSERT INTO Propuestas values('Ayuda a Venezuela',
't is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using Content here, content here, making it look like readable English. Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text',
'20191017','20191224','123575554',2,'AyudaVenezuela.jpg',1,0,5)
GO

INSERT INTO PropuestasDonacionesInsumos values (5,'Alimentos',1000)
INSERT INTO PropuestasDonacionesInsumos values (5,'Ropas',500)
GO
