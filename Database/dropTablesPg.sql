DROP SCHEMA IF EXISTS public CASCADE;
CREATE SCHEMA public;

GRANT ALL ON SCHEMA public TO evoflare;
GRANT ALL ON SCHEMA public TO public;

DROP SCHEMA IF EXISTS security CASCADE;
CREATE SCHEMA security;
GRANT ALL ON SCHEMA security TO evoflare;

DROP SCHEMA IF EXISTS core CASCADE;
CREATE SCHEMA core;
GRANT ALL ON SCHEMA core TO evoflare;
