


if exists (select * from sysobjects where name='proc_update_mima')
drop proc proc_update_mima
go
create proc proc_update_mima(
@uname nvarchar(20),
@newpwd nvarchar(20),
@hd1 nvarchar(20)
)as
declare @i int
select @i=count(*) from User_YG where mimapwd=@hd1 and UserName=@uname
if @i>0
	update User_YG set UserPwd=@newpwd where UserName=@uname


--修改用户信息
if exists(select *from sysobjects where name='proc_update_yg')
drop proc proc_update_yg
go
create proc proc_update_yg(
@gonghao int,
@name nvarchar(20),
@roid int,
@sex nvarchar(20),
@bumenid int,
@age int,
@xueli nvarchar(20),
@pwd nvarchar(20),
@idshenf nvarchar(20),
@gongzi money,
@mibaowenti nvarchar(20),
@mibaopwd nvarchar(20),
@id int
)as

update User_YG set GhaoID=@gonghao, UserName=@name,RoleID=@roid,Sex=@sex,DeptID=@bumenid,Age=@age,
Education=@xueli,UserPwd=@pwd,IDCar=@idshenf,Salary=@gongzi,mibaowenti=@mibaowenti,mimapwd=@mibaopwd where UserID=@id

exec proc_update_yg 20190602,'张无忌',2,'男',1,24,'大专',111111,'43223343543543',3243,'你喜欢的运动','跑步',9


--查询请假信息

if exists (select * from sysobjects where name='proc_select_qj')
drop proc proc_select_qj
go
create proc proc_select_qj(
@name nvarchar(20)
)as
select a.ApplyID,a.UserID,a.AttendanceID,a.Applykaishi,a.Applyjieshu,a.ApplyType,a.ShenHe from Apply_jiludan a,[dbo].[User_YG] b 
where a.UserID=b.UserID and b.UserName=@name
exec proc_select_qj '李四'


--申请请假
if exists (select * from sysobjects where name='proc_add_qj')
drop proc proc_add_qj
go
create proc proc_add_qj(
@name nvarchar(20),
@leixngid int,
@time1 datetime,
@time2 datetime,
@qjsy nvarchar(20),
@qjtype nvarchar(20),
@shenghe nvarchar(20),
@shenghe2 nvarchar(20)
)as
declare @id int
select @id=UserID from [dbo].[User_YG] where UserName=@name
insert into Apply_jiludan values(@id,@leixngid,@time1,@time2,@qjsy,@qjtype,@shenghe,@shenghe2)

exec proc_add_qj '王五',1,'2019-02-23','2019-02-24','家中有事','事假','未审核','未审核'



--申请调休
if exists (select * from sysobjects where name='proc_add_tiaoxiu')
drop proc proc_add_tiaoxiu
go
create proc proc_add_tiaoxiu(
@name nvarchar(20),
@time1 datetime,
@time2 datetime,
@anpai nvarchar(50),
@shenghe nvarchar(20),
@shenghe2 nvarchar(20)
)as
declare @id int
select @id=UserID from [dbo].[User_YG] where UserName=@name
insert into Tiaoxiu values(@id,@time1,@time2,@anpai,@shenghe,@shenghe2)

exec proc_add_tiaoxiu '张无忌','2019-02-23','2019-02-24','家中有事','未审核','未审核'



--申请外勤
if exists (select * from sysobjects where name='proc_add_waiqin')
drop proc proc_add_waiqin
go
create proc proc_add_waiqin(
@name nvarchar(20),
@time1 datetime,
@time2 datetime,
@dizhi nvarchar(20),
@liyou nvarchar(20),
@shenghe nvarchar(20),
@shenghe2 nvarchar(20)
)as
declare @id int
select @id=UserID from [dbo].[User_YG] where UserName=@name
insert into Waiqing values(@id,@time1,@time2,@dizhi,@liyou,@shenghe,@shenghe2)

exec proc_add_waiqin '麻子','2019-02-23','2019-02-24','北京','技术交流','未审核','未审核'


--出差外勤
if exists (select * from sysobjects where name='proc_add_chuchai')
drop proc proc_add_chuchai
go
create proc proc_add_chuchai(
@name nvarchar(20),
@dizhi nvarchar(20),
@liyou nvarchar(20),
@time1 datetime,
@time2 datetime,
@jieguo nvarchar(50),
@zhichi nvarchar(50),
@shenghe nvarchar(20),
@shenghe2 nvarchar(20)
)as
declare @id int
select @id=UserID from [dbo].[User_YG] where UserName=@name
insert into Chuchai values(@id,@dizhi,@liyou,@time1,@time2,@jieguo,@zhichi,@shenghe,@shenghe2)

exec proc_add_chuchai '麻子','北京','技术交流','2019-02-23','2019-02-24','学到技术','5000元','未审核','未审核'
