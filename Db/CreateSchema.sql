SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;


CREATE SCHEMA IF NOT EXISTS dbo;


-- configure role for read/write data
GRANT USAGE, CREATE ON SCHEMA dbo TO read_write;
GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA dbo TO read_write;
ALTER DEFAULT PRIVILEGES IN SCHEMA dbo GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO read_write;

GRANT USAGE ON ALL SEQUENCES IN SCHEMA dbo TO  read_write;
ALTER DEFAULT PRIVILEGES IN SCHEMA dbo GRANT USAGE ON SEQUENCES TO  read_write;


DROP ROLE IF EXISTS db_user;

CREATE ROLE db_user WITH
  LOGIN
  NOSUPERUSER
  INHERIT
  NOCREATEDB
  NOCREATEROLE
  NOREPLICATION
  NOBYPASSRLS
  ENCRYPTED PASSWORD 'SCRAM-SHA-256$4096:6Uu3FDZA/z5fl4joD3TpoQ==$vaYJ6W8Q0KBU+RJ/63ebHKL9z3W6hfdYGRCMS64i0jE=:ZKiJNRKplFNRLS0kdWg7Myf/PkhfQyPTS/nkFRc5jr8=';

GRANT read_write TO db_user;

COMMENT ON ROLE db_user IS 'User for login in db';
