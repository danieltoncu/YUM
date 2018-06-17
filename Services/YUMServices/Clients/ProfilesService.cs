using DataAccess.Infrastructure.Authorization;
using DataAccess.Model;
using Services.YUMServices.Base;
using Services.YUMServices.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccess.Infrastructure.Authorization.AuthorizationManager;

namespace Services.YUMServices.Clients
{
    public class ProfilesService : BaseAuthorizedService
    {
        public ProfilesService(string userName) : base(userName, UserRole.CLIENT)
        {
        }

        public UserProfile Get()
        {
            return _unitOfWork.UserProfileRepository.Get(_id);
        }

        public void Update(UserProfile userProfile)
        {
            userProfile.UserProfileId = _id;

            _unitOfWork.UserProfileRepository.Update(userProfile);

            _unitOfWork.Complete();
        }

        public IEnumerable<Allergen> GetAllergens()
        {
            return _unitOfWork.UserProfileRepository.Get(_id).Allergens;
        }

        public void AddAllergen(int allergenId)
        {
            Allergen allergen = _unitOfWork.AllergenRepository.Get(allergenId);

            if (allergen == null)
            {
                throw new EntityNotFoundException("Allergen with id " + allergenId + " not found.");
            }

            /*
            var items = _unitOfWork.UserProfileRepository.Get(
                up => (up.UserProfileId == _id) && up.Allergens.Any(a => a.AllergenId == allergenId));

            if (items.Any())
            {
                return;
            }
            */

            UserProfile userProfile = _unitOfWork.UserProfileRepository.Get(_id);

            userProfile.Allergens.Add(allergen);

            _unitOfWork.Complete();
        }

        public void RemoveAllergen(int allergenId)
        {
            Allergen allergen = _unitOfWork.AllergenRepository.Get(allergenId);

            if (allergen == null)
            {
                throw new EntityNotFoundException("Allergen with id " + allergenId + " not found.");
            }

            /*
            var items = _unitOfWork.UserProfileRepository.Get(
                up => (up.UserProfileId == _id) && up.Allergens.Any(a => a.AllergenId == allergenId));

            if (!items.Any())
            {
                return;
            }
            */

            UserProfile userProfile = _unitOfWork.UserProfileRepository.Get(_id);

            userProfile.Allergens.Remove(allergen);

            _unitOfWork.Complete();
        }
    }
}
