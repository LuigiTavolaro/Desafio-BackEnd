DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_namespace WHERE nspname = 'desafio-backend') THEN
        CREATE SCHEMA "desafio-backend" AUTHORIZATION guest;
    END IF;
END $$;