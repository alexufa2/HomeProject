CREATE DATABASE contractsDB
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Russian_Russia.1251'
    LC_CTYPE = 'Russian_Russia.1251'
    LOCALE_PROVIDER = 'libc'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;

COMMENT ON DATABASE contractsDB
    IS 'DB for contracts data';

GRANT TEMPORARY, CONNECT ON DATABASE contractsDB TO PUBLIC;

GRANT ALL ON DATABASE contractsDB TO postgres;


DROP ROLE IF EXISTS read_write;
-- Read-write
CREATE ROLE read_write;

GRANT CONNECT ON DATABASE contractsDB TO read_write;