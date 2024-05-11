py -m venv .env

PS C:\Users\daniel.apo\Downloads\projetos\Api-Gateway-Ocelot-Tutorial\Simple-Python-Api> .\.env\Scripts\activate    

Deseja executar o software deste fornecedor não confiável?
O arquivo C:\Users\daniel.apo\Downloads\projetos\Api-Gateway-Ocelot-Tutorial\Simple-Python-Api\.env\Scripts\Activate.ps1 é publicado por      
CN=Python Software Foundation, O=Python Software Foundation, L=Beaverton, S=Oregon, C=US e não é confiável para o seu sistema. Execute apenas 
scripts de fornecedores confiáveis.
[X] Nunca executar  [N] Não executar  [R] Executar uma vez  [A] Sempre executar  [?] Ajuda (o padrão é "N"): A

Escolha A

(.env) PS C:\Users\daniel.apo\Downloads\projetos\Api-Gateway-Ocelot-Tutorial\Simple-Python-Api> 

Se houver erro "Execução de scripts foi desabilitada neste sistema"?, use:
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser

python.exe -m pip install --upgrade pip

pip install -U Flask

