# Cadastro de Clientes - .NET MAUI

---

## 📝 Requisitos Atendidos

1. **MVVM (Model-View-ViewModel)** como principal padrão de arquitetura.  
2. Classe `Client` com os atributos:  
   - Name  
   - Lastname  
   - Age  
   - Address  
3. Tela inicial exibindo a **lista de clientes**, permitindo:  
   - Inclusão  
   - Alteração  
   - Exclusão (com confirmação por alerta)  
4. Janelas de cadastro (inclusão/edição) abrem em **nova janela** e fecham ao salvar ou cancelar.  
5. Utilização de **injeção de dependência (DI)**.  
6. Repositório publicado no **GitHub público**.  

---

## ⚡ Funcionalidades Opcionais Implementadas

- [x] Persistência local em banco SQLite (dados não se perdem entre execuções).  
- [x] Testes unitários (xUnit).  
- [x] Validação de dados (ex: `Age` deve ser numérico e entre 1–150).  
- [x] Janela principal abre maximizada e janelas de cadastro centralizadas na tela.  

---

## 🛠️ Tecnologias Utilizadas

.NET MAUI 9
UraniumUI

InputKit.Maui

CommunityToolkit.Mvvm

sqlite-net-pcl

xUnit
