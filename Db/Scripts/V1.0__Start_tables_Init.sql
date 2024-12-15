CREATE TABLE dbo.company_purpose
(
id serial primary key,
name varchar(150) NOT NULL,
description varchar(500) NOT NULL
);

-- Creating index on dbo.company_purpose
CREATE INDEX company_purpose_ind ON dbo.company_purpose (id);
ALTER TABLE dbo.company_purpose CLUSTER ON company_purpose_ind;


CREATE TABLE dbo.company
(
id serial primary key,
name varchar(150) NOT NULL,
inn varchar(150) NOT NULL,
address varchar(150) NOT NULL,
purpose_id int,
FOREIGN KEY (purpose_id) REFERENCES dbo.company_purpose (id) ON DELETE SET NULL ON UPDATE CASCADE
);

-- Creating index on dbo.company
CREATE INDEX company_ind ON dbo.company (id);
ALTER TABLE dbo.company CLUSTER ON company_ind;


CREATE TABLE dbo.good
(
id serial primary key,
name varchar(150) NOT NULL,
description varchar(500) NOT NULL,
measurement_unit varchar(150) NOT NULL
);

-- Creating index on dbo.good
CREATE INDEX good_ind ON dbo.good (id);
ALTER TABLE dbo.good CLUSTER ON good_ind;


CREATE TABLE dbo.company_goods
(
id serial primary key,
company_id int NOT NULL,
good_id int NOT NULL,
price money NOT NULL,
FOREIGN KEY (company_id) REFERENCES dbo.company (id) ON DELETE SET NULL ON UPDATE CASCADE,
FOREIGN KEY (good_id) REFERENCES dbo.good (id) ON DELETE SET NULL ON UPDATE CASCADE
);

-- Creating index on dbo.company_goods
CREATE INDEX company_goods_ind ON dbo.company_goods (company_id, good_id);
ALTER TABLE dbo.company_goods CLUSTER ON company_goods_ind;
