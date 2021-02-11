# SimF4R #

Software de gestão de parametros para controladora (Hardware de Simuladores e Drive de Potência)

## Tela 01
  --> Tela de Principal <--
  Versão 0.3 
### Controle Trava de Giro
Para que haja o travamento do giro:
 * PWM x (Angulo setado / 2)

### Max & Min eixos de PWM
Ação para definir values de ação para eixo de force do force FeedBack:
 * Min Força => Efeito de minimas frequencias de movimento
 * Man Força => Acionamento de pwm do equipamento

 ### Configurações de EIXO PEdasi e PERIFERICOS
Definição de efeito mnimo e maximo de eicos analogicos 12, 10 e 8 bit via barra deslizante:

* Min & Max => Acelerador
* Min & Max => Freio
* Min & Max => Embreagem / ativado via checkbox
* Min & Max => Freio de Mão / ativado via checkbox

### Configuração de angulo de giro
Parametrização de giro total de eixo para volante:

* Minimo 180 || Maximo 2160 => Barra deslizante
* Imagem de Aro F1 para indicação de movimento


### Efeitos
Ação para atenuação de efeito individual vs Efeito padrão Simulador/Game:

* Min (0%) & Max (80%) => Efeito de Ganho (Constante)

* Min (0%) & Max (80%) => Efeito de Molas (Torção)

* Min (0%) & Max (80%) => Efeito de Textura (Destalhes piso)

* Min (0%) & Max (80%) => Efeito deAmortecimentores (Suspeção)

* Min (0%) & Max (80%) => Efeito de Atrito (Pneus)

* Min (0%) & Max (80%) => Efeito de Inércia (Destracionamento)

### TELA 2 Câmbio e MISC