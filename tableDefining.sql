
create table Usr
(
user_id number,
userName varchar2(50) not null,
userPassword varchar2(50) not null,
userAccount varchar2(50) unique not null,
isBanned number(1) not null,
userPoint number,
themeType number,
avatar bfile,
signDate date,
createDateTime date not null,
userSecQue varchar2(50) not null,
userSecAns varchar2(50) not null,
primary key (user_id)
);

create table Admins
(
admin_id number,
adminName varchar2(50) not null,
adminPassword varchar2(50) not null,
avatar bfile,
createDateTime date not null,
primary key (admin_id)
);




create table Players
(
player_id number,
playerName varchar2(50),
playerAge number,
scoreNum number,
assistNum number,
shootNum number,
passNum number,
breakThroughNum number,
yellowNum number,
redNum number,
foulNum number,
saveNum number,
primary key (player_id)
);

create table PlayerGlory
(
player_id number,
gloryName varchar2(20),
primary key (player_id,gloryName),
foreign key (player_id) references Players
);

create table Coach
(
coach_id number,
coachName varchar2(50),
coachYear number,
primary key (coach_id)
);

create table CoachGlory
(
coach_id number,
gloryName varchar2(20),
primary key (coach_id,gloryName),
foreign key (coach_id) references coach
);


create table Team
(
team_id number,
modifyDateTime date,
teamName varchar2(20),
city varchar2(20),
coach_id number,
manager varchar2(20),
boss varchar2(20),
primary key (team_id,modifyDateTime),
foreign key(coach_id) references Coach
);

create table Game
(
game_id number,
homeTeam number ,
homeTeamModifyDateTime date,
guestTeam number ,
guestTeamModifyDateTime date,
gameName varchar2(20),
gameType number,
status number,
dateTime date,
city varchar2(20),
mainReferee varchar2(20),
primary key (game_id),
foreign key (homeTeam,homeTeamModifyDateTime) references Team(team_id,modifyDateTime),
foreign key (guestTeam,guestTeamModifyDateTime) references Team(team_id,modifyDateTime)
);

create table TeamOwnPlayer
(
team_id number,
modifyDateTime date,
player_id number,
player_type number not null,
primary key (team_id,modifyDateTime,player_id),
foreign key (team_id,modifyDateTime) references Team,
foreign key (player_id) references Players
);


create table TeamJoinGame
(
team_id number,
modifyDateTime date,
game_id number,
join_type number not null,
primary key (team_id,modifyDateTime,game_id),
foreign key (team_id,modifyDateTime) references Team,
foreign key (game_id) references Game
);


create table Teach
(
coach_id number,
team_id number,
modifyDateTime date,
primary key (coach_id, team_id, modifyDateTime),
foreign key (coach_id) references Coach,
foreign key (team_id, modifyDateTime) references Team
);


create table PlayerJoinGame
(
game_id number,
player_id number,
assistNum number,
shootNum number,
breakThroughNum number,
yellowNum number,
redNum number,
foulNum number,
saveNum number,
primary key (game_id, player_id),
foreign key (game_id) references Game,
foreign key (player_id) references Players
);

create table UserFavouritePlayer
(
user_id number,
player_id number,
primary key (user_id, player_id),
foreign key (user_id) references Usr,
foreign key (player_id) references Players
);

create table UserFavouriteTeam
(
user_id number,
team_id number,
modifyDateTime date,
primary key (user_id, team_id,modifyDateTime),
foreign key (user_id) references Usr,
foreign key (team_id,modifyDateTime) references Team(team_id,modifyDateTime)
);

create table UserScoringGame
(
user_id number,
game_id number,
score number,
primary key (user_id, game_id),
foreign key (user_id) references Usr,
foreign key (game_id) references Game
);

create table UserScoringPlayer
(
user_id number,
player_id number,
score number,
primary key (user_id, player_id),
foreign key (user_id) references Usr,
foreign key (player_id) references Players
);


create table Posts
(
post_id number,
publishDateTime date not null,
contains varchar2(1000) not null,
isBanned number(1) not null,
pictureBox bfile,
approvalNum number,
disapprovalNum number,
favouriteNum number,
primary key (post_id)
);

create table Tag
(
post_id number,
tagName varchar2(20),
primary key(post_id,tagName),
foreign key (post_id) references Posts
);

create table Comments
(
comment_id number,
publishDateTime date not null,
contains varchar2(1000) not null,
post_id number,
user_id number,
status number,
primary key (comment_id),
foreign key (post_id) references Posts,
foreign key (user_id) references Usr
);

create table Guess
(
guess_id number,
game_id number,
homeWagerNum number,
guestWagerNum number,
primary key (guess_id),
foreign key (game_id) references Game
);

create table Have
(
post_id number,
comment_id number,
primary key (post_id, comment_id),
foreign key (post_id) references Posts,
foreign key (comment_id) references Comments
);


create table UserJoinGuess
(
guess_id number,
user_id number,
guessType number not null,
primary key (guess_id,user_id),
foreign key (guess_id) references Guess,
foreign key (user_id) references Usr
);

create table PublishPost
(
post_id number,
user_id number,
primary key (post_id,user_id),
foreign key (post_id) references Posts,
foreign key (user_id) references Usr
);


create table PublishComment
(
comment_id number,
user_id number,
primary key (comment_id,user_id),
foreign key (comment_id) references Comments,
foreign key (user_id) references Usr
);

create table Reports
(
admin_id number,
reporter_id number,
reportee_id number,
report_time timestamp not null,
descriptions varchar2(200),
reply varchar2(200),
primary key(admin_id,reporter_id,reportee_id),
foreign key (admin_id) references Admins(admin_id),
foreign key (reporter_id) references Usr(user_id),
foreign key (reportee_id) references Usr(user_id)
);

create table Follow
(
follower_id number,
follow_id number,
primary key(follower_id,follow_id),
foreign key(follower_id) references Usr(user_id),
foreign key(follow_id) references Usr(user_id)
);



