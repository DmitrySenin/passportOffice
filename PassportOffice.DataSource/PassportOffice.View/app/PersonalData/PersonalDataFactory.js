;(function() {
	angular
		.module('main')
		.factory("PersonalInfoService", PersonalInfoService);

	PersonalInfoService.$inject = ['$http', 'URLService'];

	function PersonalInfoService($http, URLService) {

		return {
			SearchingOptions : SearchingOptions,
			Load : Load
		};
		
		/**
		 * Create instance of object represents searching criteria.
		 * @return {Object} Object which fields contatins restirction of searching.
		 */
		function SearchingOptions() {
			this.FirstName = null;
			this.LastName = null;
			this.MiddleName = null;
			this.BirthdayDate = null;
			this.PassportSeries = null;
			this.PassportNumber = null;
		};

		/**
		 * Carries out request for getting information from passed URL.
		 * @param {Numeric} pageSize Count of records on one page.
		 * @param {Numeric} pageNumber Number 
		 * @param {Boolean} fullSort Flag to sorting data by all fields.
		 * @return {Promise}     Promise which represents flow of request.
		 */
		function Load(pageSize, pageNumber, fullSort, searchingOptions) {
			return $http.get(URLService.BuildPersonalInfoGetURL(pageSize, pageNumber, fullSort), { params : searchingOptions });
		};
	};
})();