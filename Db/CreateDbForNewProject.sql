CREATE DATABASE contractsCheckerDB
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Russian_Russia.1251'
    LC_CTYPE = 'Russian_Russia.1251'
    LOCALE_PROVIDER = 'libc'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;

COMMENT ON DATABASE contractsCheckerDB
    IS 'DB for contracts data for check';

GRANT TEMPORARY, CONNECT ON DATABASE contractsCheckerDB TO PUBLIC;

GRANT ALL ON DATABASE contractsCheckerDB TO postgres;

GRANT CONNECT ON DATABASE contractsCheckerDB TO read_write;

