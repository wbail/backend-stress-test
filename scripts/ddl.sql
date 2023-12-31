CREATE EXTENSION pg_trgm;

CREATE TEXT SEARCH CONFIGURATION LOOKFOR (COPY = english);
ALTER TEXT SEARCH CONFIGURATION LOOKFOR ALTER MAPPING FOR hword, hword_part, word WITH english_stem;

CREATE OR REPLACE FUNCTION ARRAY_TO_STRING_IMMUTABLE (
  arr TEXT[],
  sep TEXT
) RETURNS TEXT IMMUTABLE PARALLEL SAFE LANGUAGE SQL AS $$
SELECT ARRAY_TO_STRING(arr, sep) $$;

CREATE TABLE IF NOT EXISTS Persons (
    Id UUID PRIMARY KEY,
    Nickname VARCHAR(32) UNIQUE NOT NULL,
    Name VARCHAR(100) NOT NULL,
    Birthdate DATE NOT NULL,
    Stack VARCHAR(32)[] NULL,
    Search TEXT GENERATED ALWAYS AS (
        Name || ' ' || Nickname || ' ' || COALESCE(ARRAY_TO_STRING_IMMUTABLE(Stack, ' '), '')
    ) STORED,
    CONSTRAINT persons_unique_nickname UNIQUE (Nickname)
);

CREATE INDEX IF NOT EXISTS idx_persons_Id ON persons (Id);
CREATE INDEX IF NOT EXISTS idx_persons_Search ON persons USING GIST (Search gist_trgm_ops);