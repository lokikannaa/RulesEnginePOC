CREATE TABLE IF NOT EXISTS "User" (
    id SERIAL PRIMARY KEY,
    api_key VARCHAR(255) NOT NULL UNIQUE,
    created_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_date TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS "Rule" (
    id SERIAL PRIMARY KEY,
    rule_name VARCHAR(255) NOT NULL,
    entitlement_id INT NOT NULL,
    is_active BOOL NOT NULL,
    criteria JSONB NOT NULL,
    actions JSONB NULL,
    created_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    FOREIGN KEY (entitlement_id) REFERENCES "Entitlement" (id)
);

ALTER TABLE "Rule"
ADD COLUMN actions jsonb;


CREATE TABLE IF NOT EXISTS "UserRule" (
    user_id INT NOT NULL,
    rule_id INT NOT NULL,
    created_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    PRIMARY KEY (user_id, rule_id),
    FOREIGN KEY (user_id) REFERENCES "User" (id) ON DELETE CASCADE,
    FOREIGN KEY (rule_id) REFERENCES "Rule" (id) ON DELETE CASCADE
);

CREATE table IF not EXISTS "Entitlement"
(
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL
);


--drop table "UserRule"
--drop table "Rule" 

