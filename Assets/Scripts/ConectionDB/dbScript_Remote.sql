USE railway;

DROP TABLE IF EXISTS profiles;

CREATE TABLE profiles(
  idProfile INTEGER NOT NULL AUTO_INCREMENT,
  nameProfile VARCHAR(255) UNIQUE,
  passwordProfile VARCHAR(255),
  matchesWon INTEGER,
  matchesLost INTEGER,
  enemiesKilled INTEGER,
  deaths INTEGER,
  scoreProfile INTEGER,
  PRIMARY KEY(idProfile)
);

Insert Into profiles (nameProfile, passwordProfile, matchesWon, matchesLost, enemiesKilled, deaths, scoreProfile) Values ('test', 'test', 1, 1, 6, 7, '10');
Insert Into profiles (nameProfile, passwordProfile, matchesWon, matchesLost, enemiesKilled, deaths, scoreProfile) Values ('test2', 'test2', 2, 1, 11, 7, '25');