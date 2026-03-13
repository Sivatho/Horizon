use [CG_P_DB_DATEDW]

-- Create Temp Table: Latest Billing Transaction
-- Drop if exists

--------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------Temp Table--------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------------------

IF Object_ID('tempdb..#AccHist') is not null drop table #AccHist;

select distinct PolicyNumber,
		iif(TrackingApplicableMonth is null or	TrackingApplicableMonth = '', 
		left(EffectiveDateKey,6), TrackingApplicableMonth) as EffectiveYM,
		sum(Amount) over (partition by Policynumber, 
		iif(TrackingApplicableMonth is null or	TrackingApplicableMonth = '', 
		left(EffectiveDateKey, 6), TrackingApplicableMonth)) as Amount
		Into 
		#AccHist
		from
		dim.D3_AccHist 
		where PolicyNumber = '612329975'  
		and IsValid = 1 

--------------------------------------------------------------------------------------------------------------------------------------------
--------------------------------------------------------------End Table--------------------------------------------------------------------
--------------------------------------------------------------------------------------------------------------------------------------------
select distinct
Keydet.PolicyNumber					        as legacypolicynumber,
Keydet.doc,							        
Keydet.RecordEffectiveStartDate		        as capturedate,
Mainmember.CellNo					        as preferedCommunicationMethod,
MainMember.Title					        AS title,
Mainmember.FileName					        as firstname,
Mainmember.LastName					        as surname,
Mainmember.IDNo						        as legalRefNo,
Mainmember.DOB						        as dateOfBirth,
Mainmember.EmailAddress				        as emailAddress,
Mainmember.CellNo					        as cellNumber,
Mainmember.ResAddressLine1			        as physicalAddress1,
Mainmember.ResAddressLine2			        as physicalAddress2,
Mainmember.ResAddressLine3			        as physicalSuburb,
Mainmember.ResAddressLine4					as physicalPostalCode,
Mainmember.PostAddressLine1					as postalAddress1,
Mainmember.PostAddressLine2					as postalAddress2,
Mainmember.PostAddressLine3					as postalSuburb,
Mainmember.PostAddressLine4					as postalCode,
Case Mainmember.Sex
	When		'F' Then 2
	When		'M' Then 1
	End										as genderCd,
Case Mainmember.IsSmoker
	When   'N' Then 1
	When   'Y' Then 2
	End										as Smoker_CD,
Case Mainmember.IsSmoker
	When   'N' Then 'Non-Smoker'
	When   'Y' Then 'Smoker'
	End										as smokerDesc,
AccHist.EffectiveDate						as lastBillingDate,
Dateadd(month, 1, AccHist.EffectiveDate	)	as lastPaidDate,
AccHist.Amount								as policyPremiumAmount,

sum(

	CASE
	When TempAccHist.Amount > 0
	Then 1 
	Else 0
	End) as premiumCount,

Case Keydet.Frequency
	When 'M' Then 12
	End									as paymentFrequency




from dim.D3_KeyDet as Keydet
join dim.D3_PolicyMainMember  as Mainmember 
on Mainmember.PolicyNumber = Keydet.PolicyNumber and Mainmember.IsActiveRecord = 1
join dim.D3_AccHist as AccHist
on AccHist.PolicyNumber = Mainmember.PolicyNumber and AccHist.IsValid = 1
join #AccHist as TempAccHist 
on TempAccHist.Policynumber = Keydet.PolicyNumber

where Keydet.PolicyNumber = '612329975' --legacypolicynumber,doc,
	group by 
Keydet.PolicyNumber					       
,Keydet.doc    
,Keydet.RecordEffectiveStartDate		        
,Mainmember.CellNo        
,MainMember.Title	        
,Mainmember.FileName	        
,Mainmember.LastName					        
,Mainmember.IDNo						        
,Mainmember.DOB						        
,Mainmember.EmailAddress				        
,Mainmember.CellNo					        
,Mainmember.ResAddressLine1			        
,Mainmember.ResAddressLine2			        
,Mainmember.ResAddressLine3			        
,Mainmember.ResAddressLine4					
,Mainmember.PostAddressLine1					
,Mainmember.PostAddressLine2					
,Mainmember.PostAddressLine3					
,Mainmember.PostAddressLine4					
,Case Mainmember.Sex				
	When		'F' Then 2		
	When		'M' Then 1		
	End										
,Case Mainmember.IsSmoker		
	When   'N' Then 1			
	When   'Y' Then 2			
	End										
,Case Mainmember.IsSmoker		
	When   'N' Then 'Non-Smoker'
	When   'Y' Then 'Smoker'	
	End										
,AccHist.EffectiveDate						
,AccHist.Amount								
,Keydet.Frequency


--select * from dim.D3_PMaster where PolicyStatus = 'A'
--select * from dim.D3_PolicyMainMember where PolicyNumber = '613690547'
