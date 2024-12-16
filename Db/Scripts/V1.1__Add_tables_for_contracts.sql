CREATE TABLE dbo.contract
(
id serial primary key,
number varchar(150) NOT NULL,
decription varchar(500) NOT NULL,
company_supplier_id int NOT NULL,
company_buyer_id int NOT NULL,
start_datetime timestamptz NOT NULL, 
finish_datetime timestamptz NOT NULL, 
status varchar(150) NOT NULL,
good_id int NOT NULL,
good_quantity int NOt NULL,
total_sum money NOT NULL,
done_sum money NOT NULL,
FOREIGN KEY (company_supplier_id) REFERENCES dbo.company (id) ON DELETE SET NULL ON UPDATE CASCADE,
FOREIGN KEY (company_buyer_id) REFERENCES dbo.company (id) ON DELETE SET NULL ON UPDATE CASCADE,
FOREIGN KEY (good_id) REFERENCES dbo.good (id) ON DELETE SET NULL ON UPDATE CASCADE
);

-- Creating index on dbo.contract
CREATE INDEX contract_id_ind ON dbo.contract (id);
ALTER TABLE dbo.contract CLUSTER ON contract_id_ind;

CREATE INDEX contract_csid_ind ON dbo.contract (company_supplier_id);
ALTER TABLE dbo.contract CLUSTER ON contract_csid_ind;

CREATE INDEX contract_number_ind ON dbo.contract (number);
ALTER TABLE dbo.contract CLUSTER ON contract_number_ind;

CREATE INDEX contract_status_ind ON dbo.contract (status);
ALTER TABLE dbo.contract CLUSTER ON contract_status_ind;


CREATE TABLE dbo.contract_done
(
id serial primary key,
contract_id int NOT NULL,
decription varchar(500) NOT NULL,
done_amount money NOT NULL,
done_datetime timestamptz NOT NULL, 
FOREIGN KEY (contract_id) REFERENCES dbo.contract (id) ON DELETE SET NULL ON UPDATE CASCADE
);

-- Creating index on dbo.company
CREATE INDEX contract_done_cid_ind ON dbo.contract_done (contract_id);
ALTER TABLE dbo.contract_done CLUSTER ON contract_done_cid_ind;

