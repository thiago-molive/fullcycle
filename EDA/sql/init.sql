CREATE DATABASE IF NOT EXISTS wallet;
CREATE DATABASE IF NOT EXISTS balancesmc;
USE wallet;

create table clients (id varchar(255), name varchar(255), email varchar(255), created_at date);
create table accounts (id varchar(255), client_id varchar(255), balance int, created_at date);
create table transactions (id varchar(255), account_id_from varchar(255), account_id_to varchar(255), amount int, created_at date);

-- Path: seed.sql
INSERT INTO clients (id, name, email, created_at) VALUES ('ac32415b-4895-4051-a95c-5e41b3d4a5ce', 'John Doe', 'jhon_doe@test.com', '2021-01-01');
INSERT INTO clients (id, name, email, created_at) VALUES ('849558b4-c8fb-4d3f-afd7-914f3e03852e', 'Jane Doe', 'jane_doe@test.com', '2021-01-01');

INSERT INTO accounts (id, client_id, balance, created_at) VALUES ('97f4af79-1c93-48a8-889c-9b355ac3209a', '849558b4-c8fb-4d3f-afd7-914f3e03852e', 100, '2021-01-01');
INSERT INTO accounts (id, client_id, balance, created_at) VALUES ('02c0fd04-e21d-4a97-9ddc-178e0691648e', 'ac32415b-4895-4051-a95c-5e41b3d4a5ce', 0, '2021-01-01');

-- Path: init.sql


USE balancesmc;

CREATE table balances (id varchar(255) primary key, account_id_from varchar(255), account_id_to varchar(255), balance_account_id_from int, balance_account_id_to int);

CREATE TABLE account_balances (
    id BIGINT PRIMARY KEY AUTO_INCREMENT,
    account_id VARCHAR(255),
    balance INT,
    version INT DEFAULT 0,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

ALTER TABLE account_balances ADD CONSTRAINT uc_account_id UNIQUE (account_id);