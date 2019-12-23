using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using AutoMapper;
using FluentValidation.AspNetCore;
using Newtonsoft.Json.Serialization;

using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Services;
using Infra;
using Infra.Repositories;

namespace Api {
  public class Startup {
    public Startup(IConfiguration configuration) {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services) {
      // Database Connections
      services.AddDbContextPool<DataContext>(options => 
          options.UseSqlServer(Configuration.GetConnectionString("DataContext")));

      services.AddControllers().AddNewtonsoftJson(options => {
          options.SerializerSettings.ContractResolver = new DefaultContractResolver();
          options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; 
      });

      // Auto Mapper Configurations
      services.AddAutoMapper(typeof(Startup).Assembly);
      
      // Cors & Mvc (FluentValidations)
      services.AddCors(options => 
          options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

      services.AddMvc().AddFluentValidation(cfg => 
          cfg.RegisterValidatorsFromAssemblyContaining<Startup>()
      ).SetCompatibilityVersion(CompatibilityVersion.Latest);

      #region Dependency Injection for Services and Repositories
      // Services and Repositories for Generic Purpose
      services.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));
      services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

      services.AddScoped<IServiceBase<AnpProduto>, ServiceBase<AnpProduto>>();
      services.AddScoped<IRepositoryBase<AnpProduto>, RepositoryBase<AnpProduto>>();

      services.AddScoped<IServiceBase<ClassLinha>, ServiceBase<ClassLinha>>();
      services.AddScoped<IRepositoryBase<ClassLinha>, RepositoryBase<ClassLinha>>();

      services.AddScoped<IServiceBase<CVeiculo>, ServiceBase<CVeiculo>>();
      services.AddScoped<IRepositoryBase<CVeiculo>, RepositoryBase<CVeiculo>>();

      services.AddScoped<IServiceBase<Dominio>, ServiceBase<Dominio>>();
      services.AddScoped<IRepositoryBase<Dominio>, RepositoryBase<Dominio>>();

      services.AddScoped<IServiceBase<Encargo>, ServiceBase<Encargo>>();
      services.AddScoped<IRepositoryBase<Encargo>, RepositoryBase<Encargo>>();

      services.AddScoped<IServiceBase<FInstalacao>, ServiceBase<FInstalacao>>();
      services.AddScoped<IRepositoryBase<FInstalacao>, RepositoryBase<FInstalacao>>();

      services.AddScoped<IServiceBase<FxEtaria>, ServiceBase<FxEtaria>>();
      services.AddScoped<IRepositoryBase<FxEtaria>, RepositoryBase<FxEtaria>>();

      services.AddScoped<IServiceBase<OpLinha>, ServiceBase<OpLinha>>();
      services.AddScoped<IRepositoryBase<OpLinha>, RepositoryBase<OpLinha>>();

      services.AddScoped<IServiceBase<Pais>, ServiceBase<Pais>>();
      services.AddScoped<IRepositoryBase<Pais>, RepositoryBase<Pais>>();

      services.AddScoped<IServiceBase<RhIndice>, ServiceBase<RhIndice>>();
      services.AddScoped<IRepositoryBase<RhIndice>, RepositoryBase<RhIndice>>();

      services.AddScoped<IServiceBase<Sistema>, ServiceBase<Sistema>>();
      services.AddScoped<IRepositoryBase<Sistema>, RepositoryBase<Sistema>>();

      services.AddScoped<IServiceBase<UComercial>, ServiceBase<UComercial>>();
      services.AddScoped<IRepositoryBase<UComercial>, RepositoryBase<UComercial>>();

      services.AddScoped<IServiceBase<Uf>, ServiceBase<Uf>>();
      services.AddScoped<IRepositoryBase<Uf>, RepositoryBase<Uf>>();

      services.AddScoped<IServiceBase<Via>, ServiceBase<Via>>();
      services.AddScoped<IRepositoryBase<Via>, RepositoryBase<Via>>();

      // Services and Repositories for Specific Purpose
      services.AddScoped<IAtendimentoService, AtendimentoService>();
      services.AddScoped<IAtendimentoRepository, AtendimentoRepository>();

      services.AddScoped<ICargoService, CargoService>();
      services.AddScoped<ICargoRepository, CargoRepository>();

      services.AddScoped<ICarroceriaService, CarroceriaService>();
      services.AddScoped<ICarroceriaRepository, CarroceriaRepository>();

      services.AddScoped<ICentroService, CentroService>();
      services.AddScoped<ICentroRepository, CentroRepository>();

      services.AddScoped<IChassiService, ChassiService>();
      services.AddScoped<IChassiRepository, ChassiRepository>();

      services.AddScoped<ICLinhaService, CLinhaService>();
      services.AddScoped<ICLinhaRepository, CLinhaRepository>();

      services.AddScoped<IContaService, ContaService>();
      services.AddScoped<IContaRepository, ContaRepository>();

      services.AddScoped<ICstCarroceriaService, CstCarroceriaService>();
      services.AddScoped<ICstCarroceriaRepository, CstCarroceriaRepository>();

      services.AddScoped<ICstChassiService, CstChassiService>();
      services.AddScoped<ICstChassiRepository, CstChassiRepository>();

      services.AddScoped<ICstCombustivelService, CstCombustivelService>();
      services.AddScoped<ICstCombustivelRepository, CstCombustivelRepository>();

      services.AddScoped<ICstPneuService, CstPneuService>();
      services.AddScoped<ICstPneuRepository, CstPneuRepository>();

      services.AddScoped<IDepartamentoService, DepartamentoService>();
      services.AddScoped<IDepartamentoRepository, DepartamentoRepository>();

      services.AddScoped<IDepreciacaoService, DepreciacaoService>();
      services.AddScoped<IDepreciacaoRepository, DepreciacaoRepository>();

      services.AddScoped<IECVeiculoService, ECVeiculoService>();
      services.AddScoped<IECVeiculoRepository, ECVeiculoRepository>();

      services.AddScoped<IEDominioService, EDominioService>();
      services.AddScoped<IEDominioRepository, EDominioRepository>();

      services.AddScoped<IEEncargoService, EEncargoService>();
      services.AddScoped<IEEncargoRepository, EEncargoRepository>();

      services.AddScoped<IEInstalacaoService, EInstalacaoService>();
      services.AddScoped<IEInstalacaoRepository, EInstalacaoRepository>();

      services.AddScoped<IEmbarcadoService, EmbarcadoService>();
      services.AddScoped<IEmbarcadoRepository, EmbarcadoRepository>();

      services.AddScoped<IEmpresaService, EmpresaService>();
      services.AddScoped<IEmpresaRepository, EmpresaRepository>();

      services.AddScoped<IESistemaService, ESistemaService>();
      services.AddScoped<IESistemaRepository, ESistemaRepository>();

      services.AddScoped<IFornecedorService, FornecedorService>();
      services.AddScoped<IFornecedorRepository, FornecedorRepository>();

      services.AddScoped<IFrotaService, FrotaService>();
      services.AddScoped<IFrotaRepository, FrotaRepository>();

      services.AddScoped<IFrotaHoraService, FrotaHoraService>();
      services.AddScoped<IFrotaHoraRepository, FrotaHoraRepository>();

      services.AddScoped<IFrotaEtariaService, FrotaEtariaService>();
      services.AddScoped<IFrotaEtariaRepository, FrotaEtariaRepository>();

      services.AddScoped<IFuFuncaoService, FuFuncaoService>();
      services.AddScoped<IFuFuncaoRepository, FuFuncaoRepository>();

      services.AddScoped<IFuncaoService, FuncaoService>();
      services.AddScoped<IFuncaoRepository, FuncaoRepository>();

      services.AddScoped<IHorarioService, HorarioService>();
      services.AddScoped<IHorarioRepository, HorarioRepository>();

      services.AddScoped<IInstalacaoService, InstalacaoService>();
      services.AddScoped<IInstalacaoRepository, InstalacaoRepository>();

      services.AddScoped<ILinhaService, LinhaService>();
      services.AddScoped<ILinhaRepository, LinhaRepository>();

      services.AddScoped<IMunicipioService, MunicipioService>();
      services.AddScoped<IMunicipioRepository, MunicipioRepository>();

      services.AddScoped<IOperacaoService, OperacaoService>();
      services.AddScoped<IOperacaoRepository, OperacaoRepository>();

      services.AddScoped<IOperacionalService, OperacionalService>();
      services.AddScoped<IOperacionalRepository, OperacionalRepository>();

      services.AddScoped<IPCoeficienteService, PCoeficienteService>();
      services.AddScoped<IPCoeficienteRepository, PCoeficienteRepository>();

      services.AddScoped<IPCombustivelService, PCombustivelService>();
      services.AddScoped<IPCombustivelRepository, PCombustivelRepository>();

      services.AddScoped<IPlanoService, PlanoService>();
      services.AddScoped<IPlanoRepository, PlanoRepository>();

      services.AddScoped<IPremissaService, PremissaService>();
      services.AddScoped<IPremissaRepository, PremissaRepository>();

      services.AddScoped<IProducaoService, ProducaoService>();
      services.AddScoped<IProducaoRepository, ProducaoRepository>();

      services.AddScoped<IProducaoMediaService, ProducaoMediaService>();
      services.AddScoped<IProducaoMediaRepository, ProducaoMediaRepository>();

      services.AddScoped<IProdutoService, ProdutoService>();
      services.AddScoped<IProdutoRepository, ProdutoRepository>();

      services.AddScoped<IPSinteseService, PSinteseService>();
      services.AddScoped<IPSinteseRepository, PSinteseRepository>();

      services.AddScoped<ISalarioService, SalarioService>();
      services.AddScoped<ISalarioRepository, SalarioRepository>();

      services.AddScoped<ISetorService, SetorService>();
      services.AddScoped<ISetorRepository, SetorRepository>();

      services.AddScoped<ISistDespesaService, SistDespesaService>();
      services.AddScoped<ISistDespesaRepository, SistDespesaRepository>();

      services.AddScoped<ISistFuncaoService, SistFuncaoService>();
      services.AddScoped<ISistFuncaoRepository, SistFuncaoRepository>();

      services.AddScoped<ITarifaService, TarifaService>();
      services.AddScoped<ITarifaRepository, TarifaRepository>();

      services.AddScoped<ITarifaModService, TarifaModService>();
      services.AddScoped<ITarifaModRepository, TarifaModRepository>();

      services.AddScoped<ITCategoriaService, TCategoriaService>();
      services.AddScoped<ITCategoriaRepository, TCategoriaRepository>();

      services.AddScoped<ITurnoService, TurnoService>();
      services.AddScoped<ITurnoRepository, TurnoRepository>();

      services.AddScoped<IVCatalogoService, VCatalogoService>();
      services.AddScoped<IVCatalogoRepository, VCatalogoRepository>();

      services.AddScoped<IVeiculoService, VeiculoService>();
      services.AddScoped<IVeiculoRepository, VeiculoRepository>();

      services.AddScoped<IVEquipamentoService, VEquipamentoService>();
      services.AddScoped<IVEquipamentoRepository, VEquipamentoRepository>();
      #endregion
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      }

      app.UseCors("AllowAll");
      app.UseHttpsRedirection();
      app.UseRouting();
      app.UseAuthorization();

      app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
  }
}
