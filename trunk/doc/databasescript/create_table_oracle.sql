-- Create table
create table ACCOUNT
(
  userid    NUMBER(10) not null,
  password    VARCHAR2(200),
  email     VARCHAR2(200),
  firstname VARCHAR2(100),
  lastname  VARCHAR2(100),
  status    NUMBER(1),
  address1     VARCHAR2(200),
  address2     VARCHAR2(200),
  city      VARCHAR2(200),
  state     VARCHAR2(200),
  zip       VARCHAR2(200),
  country   VARCHAR2(200),
  phone     VARCHAR2(200),
  primary key (USERID)
)
