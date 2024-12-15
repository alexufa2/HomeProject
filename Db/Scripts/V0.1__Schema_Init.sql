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
GRANT USAGE, CREATE ON SCHEMA dbo TO read_write_auto;
GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA dbo TO read_write_auto;
ALTER DEFAULT PRIVILEGES IN SCHEMA dbo GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO read_write_auto;

GRANT USAGE ON ALL SEQUENCES IN SCHEMA dbo TO  read_write_auto;
ALTER DEFAULT PRIVILEGES IN SCHEMA dbo GRANT USAGE ON SEQUENCES TO  read_write_auto;


DROP ROLE IF EXISTS auto_user;

CREATE ROLE auto_user WITH
  LOGIN
  NOSUPERUSER
  INHERIT
  NOCREATEDB
  NOCREATEROLE
  NOREPLICATION
  NOBYPASSRLS
  ENCRYPTED PASSWORD 'SCRAM-SHA-256$4096:VG2dVYSoXp4nRZAqr8MYsg==$LyHFR7Nc4YjYYdNMKxLJd8AeZRRiT3iSKxbfaStpi/M=:U0Rw+e1cwJBx+ZAFBAh3aDaQKJthtUMuHjJYdR3N+3o=';

GRANT  read_write_auto TO auto_user;
COMMENT ON ROLE auto_user IS 'User for login in auto db';