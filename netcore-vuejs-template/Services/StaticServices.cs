//using System.Collections.Generic;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using WeighingSystemCore.Models;
//using WeighingSystemCoreHelpers.Extensions;
//using WeighingSystemCoreHelpers.Enums;
//using WeighingSystemCore;
//using System.Linq;
//using System;

//namespace WeighingSystemCore.Services
//{
//    public class StaticClassListServices
//    {

//        private IBinLocationRepository binLocationRepository;
//        private IPalletRepository palletRepository;
//        private IPackagingTypeRepository packagingTypeRepository;
 
//        public StaticClassListServices(
//          IBinLocationRepository _binLocationRepo,
//          ILocationRepository _locationRepository,
//          IPalletRepository _palletRepo,
//          IRawMaterialRepository _rawMatsRepository,
//          IWarehouseRepository _whRepository,
//          IPackagingTypeRepository _packagingTypeRepo,
//          IGenSettingsRepository _genSettingsRepository,
//          IWeighingAreaRepository _weighingAreaRepository,
//          IShiftRepository _shiftRepository)
//        {
//            binLocationRepository = _binLocationRepo;
//            locRepository = _locationRepository;
//            palletRepository = _palletRepo;
//            rawMaterialRepository = _rawMatsRepository;
//            whRespository = _whRepository;
//            packagingTypeRepository = _packagingTypeRepo;
//            genSettingsRepository = _genSettingsRepository;
//            weighingAreaRepository = _weighingAreaRepository;
//            shiftRepository = _shiftRepository;
//        }

//        public List<BinLocation> ListBinLocations()
//        {
//            return binLocationRepository.List().ToList();
//        }

//        public List<Pallet> ListPallets()
//        {
//            return palletRepository.List().ToList();
//        }

//        public List<PackagingType> ListPackagingTypes()
//        {
//            return packagingTypeRepository.List().ToList();
//        }
//    }
//}