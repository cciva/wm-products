# wm-demo
App for browsing and displaying various products


To setup db environment, use following script : 'Shop/Shop/Store/db/ShopDb.sql'
Json files are here 'Shop/Shop/Store/files/*.json'

To switch between sql store and file store (json), do the following :

1. for sql server store uncomment this line in web.config (other one should be commented)
<repoConfig id="products" type="SqlDb" params="Data Source=.\SQLEXPRESS;Database=ShopDb;User Id=wm;Password=wm" />

2. for file store (json) uncomment this line in web.config (other one should be commented)
<repoConfig id="products" type="JsonFile" />