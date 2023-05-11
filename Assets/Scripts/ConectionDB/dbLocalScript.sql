DROP TABLE IF EXISTS profiles;

CREATE TABLE profiles(
  idProfile INTEGER NOT NULL AUTO_INCREMENT,
  nameProfile VARCHAR(255) UNIQUE,
  passwordProfile VARCHAR(255),
  isLogged BOOLEAN,
  scoreProfile INTEGER,
  PRIMARY KEY(idProfile)
);

Insert Into profiles (nameProfile, passwordProfile, isLogged, scoreProfile) 
Values ('test', 'test', FALSE, '10'),
('test2', 'test2', FALSE, '25');