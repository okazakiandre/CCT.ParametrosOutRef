// See https://aka.ms/new-console-template for more information
using CCT.ParametrosOutRef.App.UseCases;

Console.WriteLine("Hello, World!");

var usc = new PreencherVendedoresUseCase();
var retorno = usc.Executar(null, null, out double percentual);
Console.WriteLine(percentual);
