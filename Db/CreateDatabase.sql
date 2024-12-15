-- Database: Auto

CREATE DATABASE auto
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    LC_COLLATE = 'Russian_Russia.1251'
    LC_CTYPE = 'Russian_Russia.1251'
    LOCALE_PROVIDER = 'libc'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;

COMMENT ON DATABASE auto
    IS 'DB for auto data';

GRANT TEMPORARY, CONNECT ON DATABASE auto TO PUBLIC;

GRANT ALL ON DATABASE auto TO postgres;


DROP ROLE IF EXISTS read_write_auto;
-- Read-write
CREATE ROLE read_write_auto;

GRANT CONNECT ON DATABASE auto TO read_write_auto;


