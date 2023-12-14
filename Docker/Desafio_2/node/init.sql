CREATE DATABASE IF NOT EXISTS fullcycle;
USE fullcycle;

CREATE TABLE IF NOT EXISTS users (id INT AUTO_INCREMENT PRIMARY KEY,username VARCHAR(255));

INSERT INTO users (username) VALUES ('Thiago'), ('Maria');
