Imports System.IO
Imports BoletoBr
Imports BoletoBr.Arquivo

Public Class Main


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

        '     Dim remessa As New Remessa(remessa.Ambiente.Homologacao, remessa.CodigoOcorrencia.Registro, 2)





    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim table_retorno As New DataTable
        table_retorno.Columns.Add("id", GetType(Integer))
        table_retorno.Columns.Add("valorlancamento", GetType(String))
        table_retorno.Columns.Add("valorliquidorecebido", GetType(String))
        table_retorno.Columns.Add("numerodocumento", GetType(String))
        table_retorno.Columns.Add("nossonumero", GetType(String))
        table_retorno.Columns.Add("datacredito", GetType(String))
        table_retorno.Columns.Add("valortituloparcela", GetType(String))
        table_retorno.Columns.Add("valorjurosdemora", GetType(String))
        table_retorno.Columns.Add("valordesconto", GetType(String))
        table_retorno.Columns.Add("ocorrencia", GetType(String))

        OpenFileDialog1.FileName = ""
        OpenFileDialog1.Title = "Selecione um arquivo de retorno"
        OpenFileDialog1.Filter = "Arquivos de Retorno (*.ret)|*.ret|Arquivos de Remessa (*.rem)|*.rem|Todos Arquivos (*.*)|*.*"

        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then

            Dim str As StreamReader = New StreamReader(OpenFileDialog1.FileName)
            Dim allLinesText As List(Of String) = File.ReadAllLines(OpenFileDialog1.InitialDirectory + OpenFileDialog1.FileName).ToList



            Dim banco = Fabricas.BancoFactory.ObterBanco("001")
            Dim objReturn = banco.LerArquivoRetorno(allLinesText)


            '  MsgBox(objReturn.RetornoCnab400Especifico.Header.NomeDoBeneficiario.ToString)
            Dim registro As Integer = objReturn.RetornoCnab400Especifico.RegistrosDetalhe.Count()

            While registro > 0
                registro = registro - 1
                table_retorno.Rows.Add(registro,
                                       objReturn.RetornoCnab400Especifico.RegistrosDetalhe(registro).ValorLancamento,
                                       objReturn.RetornoCnab400Especifico.RegistrosDetalhe(registro).ValorLiquidoRecebido,
                                       objReturn.RetornoCnab400Especifico.RegistrosDetalhe(registro).NumeroDocumento,
                                       objReturn.RetornoCnab400Especifico.RegistrosDetalhe(registro).NossoNumero,
                                       objReturn.RetornoCnab400Especifico.RegistrosDetalhe(registro).DataDeCredito,
                                       objReturn.RetornoCnab400Especifico.RegistrosDetalhe(registro).ValorDoTituloParcela,
                                       objReturn.RetornoCnab400Especifico.RegistrosDetalhe(registro).ValorJurosDeMora,
                                       objReturn.RetornoCnab400Especifico.RegistrosDetalhe(registro).ValorDesconto,
                                       objReturn.RetornoCnab400Especifico.RegistrosDetalhe(registro).CodigoDeOcorrencia)



            End While





        End If



        DataGridView1.DataSource = table_retorno


    End Sub
End Class
