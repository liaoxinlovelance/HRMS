--创建数据库
create database HrmDB

use HrmDB
go

--创建权限表
create table Module_quanxian(
ModuleID int not null primary key identity(1,1),
ModuleName nvarchar(50) not null,
TypeID int foreign key references Module_quanxian(ModuleID)
)

--添加信息
insert into Module_quanxian values
('所有权限',null),('查看员工信息',1),('维护员工信息',1),('审核考勤',1),
('员工信息调阅',2),('员工信息汇总',2),('员工信息导出',2),
('添加员工信息',3),('修改员工信息',3),('删除信息信息',3),
('审核部门考勤',4),('审核校区考核',4),('调阅考勤记录',4)


--创建角色表
create table Role_juese(
RoleID int not null primary key identity(1,1),
RoleName nvarchar(50) not null,
ModuleID int foreign key references Module_quanxian(ModuleID) not null
)

--添加角色信息
insert into Role_juese values
('校区主任',1),('部门主管',2),('部门人员',3)


--创建部门表
create table Dept_bumen(
DeptID int not null primary key identity(1,1),
DeptName nvarchar(20) not null
)


--创建员工表
create table User_YG(
UserID int not null primary key identity(1,1),
GhaoID int not null,
UserName nvarchar(20) not null,
RoleID int foreign key references Role_juese(RoleID) not null,
DeptID int foreign key references Dept_bumen(DeptID) not null,
UserPwd nvarchar(20) not null,
IDCar nvarchar(50) not null,
Sex nvarchar(4) check(Sex='男' or Sex='女') not null,
Age int not null,
Starttime date not null,
Education nvarchar(10) check(Education='中专' or Education='大专' or Education='本科' or Education='硕士' or Education='博士') not null,
Salary money not null,
mibaowenti nvarchar(50) not null,
mimapwd nvarchar(20) not null
)

--创建记录单类别表
create table Apply_Type(
AttendanceID int not null primary key identity(1,1),
AttendanceName nvarchar(50) not null
)

insert into Apply_Type values
('休假'),
('缺打卡'),
('调休'),
('外勤'),
('出差')

--创建休假记录单表
create table Apply_jiludan(
ApplyID int not null primary key identity(1,1),
UserID int foreign key references User_YG(UserID) not null,
AttendanceID int foreign key references Apply_Type(AttendanceID) not null,
Applykaishi date not null,
Applyjieshu date not null,
Applyyuanyin nvarchar(100) not null,
ApplyType nvarchar(10) check(ApplyType='病假' or ApplyType='事假' or ApplyType='婚假' or ApplyType='工伤假' or ApplyType='产假' or ApplyType='产检假' or ApplyType='丧假') not null,
ShenHe nvarchar(20) check(ShenHe='部门同意' or ShenHe='部门不同意' or ShenHe='未审核' or ShenHe='校区同意' or ShenHe='校区不同意'),
)

insert into Apply_jiludan values
(4,1,'2018-2-20','2018-2-25','回家有事情','病假','未审核')



--创建调休记录单表
create table Tiaoxiu(
ApplyID int not null primary key identity(1,1),
UserID int foreign key references User_YG(UserID) not null,
Applykaishi date not null,
Applyjieshu date not null,
Applyanpai nvarchar(100) not null,
BumenShenHe nvarchar(20) check(BumenShenHe='未审核' or BumenShenHe='部门同意' or BumenShenHe='部门不同意'),
XiaoquShenHe nvarchar(20) check(XiaoquShenHe='未审核' or XiaoquShenHe='校区同意' or XiaoquShenHe='校区不同意')
)

insert into Tiaoxiu values
(3,'2018-2-20','2018-2-25','无安排','未审核','未审核')

--创建外勤记录单
create table Waiqing(
ApplyID int not null primary key identity(1,1),
UserID int foreign key references User_YG(UserID) not null,
Applykaishi date not null,
Applyjieshu date not null,
Applydizhi nvarchar(20) not null,
ApplyLiyou nvarchar(100) not null,
BumenShenHe nvarchar(20) check(BumenShenHe='未审核' or BumenShenHe='部门同意' or BumenShenHe='部门不同意'),
XiaoquShenHe nvarchar(20) check(XiaoquShenHe='未审核' or XiaoquShenHe='校区同意' or XiaoquShenHe='校区不同意')
)

insert into Waiqing values
(3,'2018-5-20','2018-5-25','长沙','去买菜','未审核','未审核')


--创建出差记录单
create table Chuchai(
ApplyID int not null primary key identity(1,1),
UserID int foreign key references User_YG(UserID) not null,
Applydizhi nvarchar(20) not null,
ApplyLiyou nvarchar(100) not null,
Applykaishi date not null,
Applyjieshu date not null,
ApplyJieguo nvarchar(100) not null,
ApplyZhichi nvarchar(100) not null,
BumenShenHe nvarchar(20) check(BumenShenHe='未审核' or BumenShenHe='部门同意' or BumenShenHe='部门不同意'),
XiaoquShenHe nvarchar(20) check(XiaoquShenHe='未审核' or XiaoquShenHe='校区同意' or XiaoquShenHe='校区不同意')
)

insert into Chuchai values
(3,'长沙','去蹦迪','2018-5-21','2018-5-22','一直蹦迪一直爽','需要2000块钱','未审核','未审核')



