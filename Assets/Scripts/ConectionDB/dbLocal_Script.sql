USE db_dualproject;

DROP TABLE IF EXISTS profiles;

CREATE TABLE profiles(
  idProfile INTEGER NOT NULL AUTO_INCREMENT,
  nameProfile VARCHAR(255) UNIQUE,
  passwordProfile VARCHAR(255),
  isLogged BOOLEAN,
  matchesWon INTEGER,
  matchesLost INTEGER,
  enemiesKilled INTEGER,
  deaths INTEGER,
  PRIMARY KEY(idProfile)
);

Insert Into profiles (nameProfile, passwordProfile, isLogged, matchesWon, matchesLost, enemiesKilled, deaths) Values ('player1', 'test1', false, '5', '4', '13', '12');
Insert Into profiles (nameProfile, passwordProfile, isLogged, matchesWon, matchesLost, enemiesKilled, deaths) Values ('player2', 'test2', false,  '4', '5', '12', '13');
Insert Into profiles (nameProfile, passwordProfile, isLogged, matchesWon, matchesLost, enemiesKilled, deaths) Values ('player3', 'test3', false, '0', '0', '0', '0');
Insert Into profiles (nameProfile, passwordProfile, isLogged, matchesWon, matchesLost, enemiesKilled, deaths) Values ('player4', 'test4', false,  '0', '0', '0', '0');