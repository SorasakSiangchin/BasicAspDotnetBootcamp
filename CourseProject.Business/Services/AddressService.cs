using AutoMapper;
using CourseProject.Common.Dtos;
using CourseProject.Common.Interfaces;
using CourseProject.Common.Models;

namespace CourseProject.Services;

public class AddressService : IAddressService
{
    private IGenericRepository<Address> _genericRepository { get; }
    private IMapper _mapper { get; }

    public AddressService(IMapper mapper , IGenericRepository<Address> genericRepository)
    {
        _genericRepository = genericRepository;
        _mapper = mapper;
    }
    public async Task<int> CreateAddressAsync(AddressCreate addressCreate)
    {
        var entity = _mapper.Map<Address>(addressCreate);
        await _genericRepository.InsertAsync(entity);
        await _genericRepository.SaveChangesAsync();
        return entity.Id;
    }

    public async Task UpdateAddressAsync(AddressUpdate addressUpdate)
    {
        // var entity = await _genericRepository.GetByIdAsync(addressUpdate.Id); 
        var entity = _mapper.Map<Address>(addressUpdate);
        _genericRepository.Update(entity);
        await _genericRepository.SaveChangesAsync();
    }

    public async Task DeleteAddressAsync(AddressDelete addressDelete)
    {
        var entity = await _genericRepository.GetByIdAsync(addressDelete.Id);
        _genericRepository.Delete(entity);
        await _genericRepository.SaveChangesAsync();
    }

    public async Task<AddressGet> GetAddressAsync(int id)
    {
        var entity = await _genericRepository.GetByIdAsync(id);
        return _mapper.Map<AddressGet>(entity);
    }

    public async Task<List<AddressGet>> GetAddressesAsync()
    {
        var entities = await _genericRepository.GetAsync(null , null);
        return _mapper.Map<List<AddressGet>>(entities);
    }
}