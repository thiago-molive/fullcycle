CREATE DATABASE IF NOT EXISTS wallet;
CREATE DATABASE IF NOT EXISTS balances;
USE wallet;

create table clients (id varchar(255), name varchar(255), email varchar(255), created_at date);
create table accounts (id varchar(255), client_id varchar(255), balance int, created_at date);
create table transactions (id varchar(255), account_id_from varchar(255), account_id_to varchar(255), amount int, created_at date);

-- Path: seed.sql
INSERT INTO CLIENTS (id, name, email, created_at) VALUES ('D32570A4-DDAD-4BF6-AF1B-18623F13F381', 'John Doe', 'jhon_doe@test.com', '2021-01-01');
INSERT INTO CLIENTS (id, name, email, created_at) VALUES ('F68DB628-A4E0-4419-BFBF-60E8E1BF797D', 'Jane Doe', 'jane_doe@test.com', '2021-01-01');

INSERT INTO ACCOUNTS (id, client_id, balance, created_at) VALUES ('CD5F8A4E-98CC-4A12-98C2-441350A1CB3D', 'D32570A4-DDAD-4BF6-AF1B-18623F13F381', 1000, '2021-01-01');
INSERT INTO ACCOUNTS (id, client_id, balance, created_at) VALUES ('AE61653A-07CE-4E10-AD26-42BE2CC2DD34', 'F68DB628-A4E0-4419-BFBF-60E8E1BF797D', 1000, '2021-01-01');

-- Path: init.sql


USE balances;

CREATE table balances (id varchar(255) primary key, account_id_from varchar(255), account_id_to varchar(255), balance_account_id_from int, balance_account_id_to int);

-- Path: seed.sql
INSERT INTO balances (ID, ACCOUNT_ID_FROM, ACCOUNT_ID_TO, BALANCE_ACCOUNT_ID_FROM, BALANCE_ACCOUNT_ID_TO) VALUES ('2E3A5DB6-EEA8-485D-A6D5-3448CF450952', 'CD5F8A4E-98CC-4A12-98C2-441350A1CB3D', 'AE61653A-07CE-4E10-AD26-42BE2CC2DD34', 1000, 1000);

