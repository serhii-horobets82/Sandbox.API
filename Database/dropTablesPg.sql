DROP SCHEMA public CASCADE;
CREATE SCHEMA public;

GRANT ALL ON SCHEMA public TO postgres;
GRANT ALL ON SCHEMA public TO public;

DROP SCHEMA security CASCADE;
CREATE SCHEMA security;
GRANT ALL ON SCHEMA security TO postgres;

DROP SCHEMA core CASCADE;
CREATE SCHEMA core;
GRANT ALL ON SCHEMA core TO postgres;