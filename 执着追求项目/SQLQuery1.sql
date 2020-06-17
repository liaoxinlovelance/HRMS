--�������ݿ�
create database HrmDB

use HrmDB
go

--����Ȩ�ޱ�
create table Module_quanxian(
ModuleID int not null primary key identity(1,1),
ModuleName nvarchar(50) not null,
TypeID int foreign key references Module_quanxian(ModuleID)
)

--�����Ϣ
insert into Module_quanxian values
('����Ȩ��',null),('�鿴Ա����Ϣ',1),('ά��Ա����Ϣ',1),('��˿���',1),
('Ա����Ϣ����',2),('Ա����Ϣ����',2),('Ա����Ϣ����',2),
('���Ա����Ϣ',3),('�޸�Ա����Ϣ',3),('ɾ����Ϣ��Ϣ',3),
('��˲��ſ���',4),('���У������',4),('���Ŀ��ڼ�¼',4)


--������ɫ��
create table Role_juese(
RoleID int not null primary key identity(1,1),
RoleName nvarchar(50) not null,
ModuleID int foreign key references Module_quanxian(ModuleID) not null
)

--��ӽ�ɫ��Ϣ
insert into Role_juese values
('У������',1),('��������',2),('������Ա',3)


--�������ű�
create table Dept_bumen(
DeptID int not null primary key identity(1,1),
DeptName nvarchar(20) not null
)


--����Ա����
create table User_YG(
UserID int not null primary key identity(1,1),
GhaoID int not null,
UserName nvarchar(20) not null,
RoleID int foreign key references Role_juese(RoleID) not null,
DeptID int foreign key references Dept_bumen(DeptID) not null,
UserPwd nvarchar(20) not null,
IDCar nvarchar(50) not null,
Sex nvarchar(4) check(Sex='��' or Sex='Ů') not null,
Age int not null,
Starttime date not null,
Education nvarchar(10) check(Education='��ר' or Education='��ר' or Education='����' or Education='˶ʿ' or Education='��ʿ') not null,
Salary money not null,
mibaowenti nvarchar(50) not null,
mimapwd nvarchar(20) not null
)

--������¼������
create table Apply_Type(
AttendanceID int not null primary key identity(1,1),
AttendanceName nvarchar(50) not null
)

insert into Apply_Type values
('�ݼ�'),
('ȱ��'),
('����'),
('����'),
('����')

--�����ݼټ�¼����
create table Apply_jiludan(
ApplyID int not null primary key identity(1,1),
UserID int foreign key references User_YG(UserID) not null,
AttendanceID int foreign key references Apply_Type(AttendanceID) not null,
Applykaishi date not null,
Applyjieshu date not null,
Applyyuanyin nvarchar(100) not null,
ApplyType nvarchar(10) check(ApplyType='����' or ApplyType='�¼�' or ApplyType='���' or ApplyType='���˼�' or ApplyType='����' or ApplyType='�����' or ApplyType='ɥ��') not null,
ShenHe nvarchar(20) check(ShenHe='����ͬ��' or ShenHe='���Ų�ͬ��' or ShenHe='δ���' or ShenHe='У��ͬ��' or ShenHe='У����ͬ��'),
)

insert into Apply_jiludan values
(4,1,'2018-2-20','2018-2-25','�ؼ�������','����','δ���')



--�������ݼ�¼����
create table Tiaoxiu(
ApplyID int not null primary key identity(1,1),
UserID int foreign key references User_YG(UserID) not null,
Applykaishi date not null,
Applyjieshu date not null,
Applyanpai nvarchar(100) not null,
BumenShenHe nvarchar(20) check(BumenShenHe='δ���' or BumenShenHe='����ͬ��' or BumenShenHe='���Ų�ͬ��'),
XiaoquShenHe nvarchar(20) check(XiaoquShenHe='δ���' or XiaoquShenHe='У��ͬ��' or XiaoquShenHe='У����ͬ��')
)

insert into Tiaoxiu values
(3,'2018-2-20','2018-2-25','�ް���','δ���','δ���')

--�������ڼ�¼��
create table Waiqing(
ApplyID int not null primary key identity(1,1),
UserID int foreign key references User_YG(UserID) not null,
Applykaishi date not null,
Applyjieshu date not null,
Applydizhi nvarchar(20) not null,
ApplyLiyou nvarchar(100) not null,
BumenShenHe nvarchar(20) check(BumenShenHe='δ���' or BumenShenHe='����ͬ��' or BumenShenHe='���Ų�ͬ��'),
XiaoquShenHe nvarchar(20) check(XiaoquShenHe='δ���' or XiaoquShenHe='У��ͬ��' or XiaoquShenHe='У����ͬ��')
)

insert into Waiqing values
(3,'2018-5-20','2018-5-25','��ɳ','ȥ���','δ���','δ���')


--���������¼��
create table Chuchai(
ApplyID int not null primary key identity(1,1),
UserID int foreign key references User_YG(UserID) not null,
Applydizhi nvarchar(20) not null,
ApplyLiyou nvarchar(100) not null,
Applykaishi date not null,
Applyjieshu date not null,
ApplyJieguo nvarchar(100) not null,
ApplyZhichi nvarchar(100) not null,
BumenShenHe nvarchar(20) check(BumenShenHe='δ���' or BumenShenHe='����ͬ��' or BumenShenHe='���Ų�ͬ��'),
XiaoquShenHe nvarchar(20) check(XiaoquShenHe='δ���' or XiaoquShenHe='У��ͬ��' or XiaoquShenHe='У����ͬ��')
)

insert into Chuchai values
(3,'��ɳ','ȥ�ĵ�','2018-5-21','2018-5-22','һֱ�ĵ�һֱˬ','��Ҫ2000��Ǯ','δ���','δ���')



