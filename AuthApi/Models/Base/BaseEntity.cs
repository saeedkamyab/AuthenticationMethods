
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using AuthApi.SpecialMethod.HttpHelper;

namespace AuthApi.Models.Base
{
    public abstract class BaseEntity : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;// It's for INotifyPropertychanged when you inheritant
        /***********************************/


        [Key] public Guid Id { get; set; }


        [NotMapped] public List<string> LoadedPropNames { get; set; } = new();

        public static async Task<(List<T>, string?)> LoadList<T>(List<string>? includes = null, Expression? expression = null) where T : BaseEntity, new()
        {
            var inputModel = new CustomRequestMessage(new T().SerializeEntity())
            {
                Includes = includes,
                Expression = new ExpressionSerializer(ExpressionSerializer).SerializeText(expression)
            };

            var response = await CustomHttpService.PostAsync("api/BaseEntity/LoadList", inputModel);
            List<T>? list = null;

            if (response.IsSuccess && !string.IsNullOrWhiteSpace(response.Data))
                list = response.Data.Deserialize<List<T>>();

            return (list ?? new List<T>(), response.ErrorMessage);
        }

        public static JsonSerializer ExpressionSerializer
        {
            get
            {
                var serializer = new JsonSerializer();
                var list = new List<Type>
            {
                typeof(ActionType),
                typeof(AdviceOfArrivalState),
                typeof(BillOfLadingState),
                typeof(BuyOrderListState),
                typeof(ContractType),
                typeof(CurrencyAllocationRequestType),
                typeof(CurrencyAllocationState),
                typeof(CurrencyAllocationTransactionType),
                typeof(CurrencyPurchaseStatus),
                typeof(CustomDeclarationState),
                typeof(CustomDeclarationStep),
                typeof(LoadingFormState),
                typeof(Messages),
                typeof(OrderRegistrationItemState),
                typeof(OrderRegistrationState),
                typeof(OrderRegistrationStep),
                typeof(PackingListState),
                typeof(PermissionType),
                typeof(PrimeCode),
                typeof(ProductStatus),
                typeof(ProformaInvoiceState),
                typeof(RequestState),
                typeof(SupplySource),
                typeof(WarehouseReceiptStatus),
                typeof(PlanBaseType),
                typeof(RemittanceToType),
            };
                serializer.AddKnownTypes(list);
                return serializer;
            }
        }

        public static async Task<(T?, string?)> LoadEntity<T>(Guid id, List<string>? includes = null)
            where T : BaseEntity, new()
        {
            var entity = new T
            {
                Id = id
            };
            var inputModel = new CustomRequestMessage(entity.SerializeEntity()) { Includes = includes };
            var response = await CustomHttpService.PostAsync("api/BaseEntity/LoadEntity", inputModel);
            T? data = null;

            if (response.IsSuccess && !string.IsNullOrWhiteSpace(response.Data))
                data = response.Data.DeserializeEntity() as T;

            return (data, response.ErrorMessage);
        }

        public virtual async Task<(BaseEntity?, string?)> Update()
        {
            var inputModel = new CustomRequestMessage(this.SerializeEntity());
            var response = await CustomHttpService.PostAsync("api/BaseEntity/Update", inputModel);

            BaseEntity? data = null;

            if (response.IsSuccess && !string.IsNullOrWhiteSpace(response.Data))
                data = response.Data.DeserializeEntity();

            return (data, response.ErrorMessage);
        }

        public virtual async Task<(BaseEntity?, string?)> Delete()
        {
            var inputModel = new CustomRequestMessage(this.SerializeEntity());
            var response = await CustomHttpService.PostAsync($"api/BaseEntity/Delete", inputModel);

            BaseEntity? data = null;

            if (response.IsSuccess && !string.IsNullOrWhiteSpace(response.Data))
                data = response.Data.DeserializeEntity();

            return (data, response.ErrorMessage);
        }


        public static async Task<(List<T>, string?)> UpdateList<T>(List<T> list)
        {
            var inputModel = new CustomRequestMessage(list.SerializeEntity());
            var response = await CustomHttpService.PostAsync("api/BaseEntity/UpdateList", inputModel);
            List<T>? resultList = null;
            if (response.IsSuccess && !string.IsNullOrWhiteSpace(response.Data))
                resultList = response.Data.Deserialize<List<T>>();
            return (resultList ?? new List<T>(), response.ErrorMessage);
        }

        public static async Task<(List<T>, string?)> DeleteList<T>(List<T> list)
        {
            var inputModel = new CustomRequestMessage(list.SerializeEntity());
            var response = await CustomHttpService.PostAsync("api/BaseEntity/DeleteList", inputModel);
            List<T>? resultList = null;
            if (response.IsSuccess && !string.IsNullOrWhiteSpace(response.Data))
                resultList = response.Data.Deserialize<List<T>>();
            return (resultList ?? new List<T>(), response.ErrorMessage);
        }

       

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            // IsDirty = true;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            // IsDirty = true;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
}
