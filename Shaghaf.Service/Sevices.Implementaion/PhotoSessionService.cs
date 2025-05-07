using AutoMapper;
using Shaghaf.Core.Entities.BirthdayEntity;
using Shaghaf.Core.Services.Contract;
using Shaghaf.Core;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shaghaf.Core.Dtos.PhotoSessionDtos;

public class PhotoSessionService : IPhotoSessionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PhotoSessionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PhotoSessionDto> CreatePhotoSessionAsync(PhotoSessionToCreateDto photoSessionDto)
    {
        var photoSession = _mapper.Map<PhotoSession>(photoSessionDto);
        _unitOfWork.Repository<PhotoSession>().AddAsync(photoSession);
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<PhotoSessionDto>(photoSession);
    }

    public async Task UpdatePhotoSessionAsync(PhotoSessionDto photoSessionDto)
    {
        var photoSession = await _unitOfWork.Repository<PhotoSession>().GetByIdAsync(photoSessionDto.Id);
        if (photoSession == null)
        {
            throw new KeyNotFoundException("No photo session found matching the specified criteria.");
        }

        _mapper.Map(photoSessionDto, photoSession);
        _unitOfWork.Repository<PhotoSession>().Update(photoSession);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<PhotoSessionDto?> GetPhotoSessionByIdAsync(int id)
    {
        var photoSession = await _unitOfWork.Repository<PhotoSession>().GetByIdAsync(id);
        return photoSession == null ? null : _mapper.Map<PhotoSessionDto>(photoSession);
    }

    public async Task<IReadOnlyList<PhotoSessionDto>> GetAllPhotoSessionsAsync()
    {
        var photoSessions = await _unitOfWork.Repository<PhotoSession>().GetAllAsync();
        return _mapper.Map<IReadOnlyList<PhotoSessionDto>>(photoSessions);
    }

    public async Task<bool> DeletePhotoSessionAsync(int id)
    {
        var photoSession = await _unitOfWork.Repository<PhotoSession>().GetByIdAsync(id);
        if (photoSession == null)
        {
            return false;
        }

        _unitOfWork.Repository<PhotoSession>().Delete(photoSession);
        await _unitOfWork.CompleteAsync();
        return true;
    }
}
