Imports BoletoBr
Imports BoletoBr.Arquivo

Public Class Form1


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click



        Dim contabb As New ContaBancaria("861", "1183")
        contabb.DigitoAgencia = "3"
        contabb.DigitoConta = "3"


        Dim enderecobb As New Endereco()
        enderecobb.Logradouro = "Rua Francisco Cardoso"
        enderecobb.Numero = "25"
        enderecobb.Bairro = "centro"
        enderecobb.Cidade = "miracema"
        enderecobb.SiglaUf = "RJ"
        enderecobb.Cep = "2846000"

        Dim carteirabb As New CarteiraCobranca()
        carteirabb.Codigo = 17



        Dim cedentebb As New Cedente(contabb.Agencia, contabb.DigitoAgencia, contabb.DigitoConta, "09223089000110", contabb, enderecobb)
        cedentebb.Convenio = "2935698"
        Dim sacado As New Sacado("david jose rosa dos santos", "09580860793", enderecobb)

        Dim bb As New Boleto

        bb.CedenteBoleto = cedentebb
        bb.CarteiraCobranca = carteirabb
        bb.DataVencimento = "20/12/2016"
        bb.ValorBoleto = 10.0


        bb.NumeroDocumento = "2222"
        bb.IdentificadorInternoBoleto = "2222222"

        bb.ValidaDadosEssenciaisDoBoleto()

        Dim banco = Fabricas.BancoFactory.ObterBanco("001", "0")

        banco.FormatarBoleto(bb)


        ' falta gerar remessa e salvar arquivo

        Dim remessa As New Remessa(remessa.Ambiente.Homologacao, remessa.CodigoOcorrencia.Registro, 2)



    End Sub
End Class
