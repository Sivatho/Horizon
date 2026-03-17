USE[Polly_C]


-- Create Temp Table: Latest Billing Transaction
-- Drop if exists
IF OBJECT_ID('tempdb..#LatestPolicyTransaction') IS NOT NULL DROP TABLE #LatestPolicyTransaction;

SELECT m1.*
INTO #LatestPolicyTransaction
FROM polmas.MPolicyTransaction m1
JOIN (
    SELECT PolicyNo, MAX(EffDate) AS LatestEffDate
    FROM polmas.MPolicyTransaction
    GROUP BY PolicyNo
) x ON m1.PolicyNo = x.PolicyNo AND m1.EffDate = x.LatestEffDate;


-- Create Temp Table: Preferred Telecom
IF OBJECT_ID('tempdb..#PreferredTelecom') IS NOT NULL DROP TABLE #PreferredTelecom;

SELECT 
    EntityNo,
    Telecom,
    TelTypeCD
INTO #PreferredTelecom
FROM Customer.cust.EntityTelecom
WHERE Preferred = 1;

-- Create Temp Table: Flattened Entity Address
IF OBJECT_ID('tempdb..#EntityAddress') IS NOT NULL DROP TABLE #EntityAddress;

SELECT 
    EntityNo,
    MAX(CASE WHEN AddrTypeCD = 1 THEN AddressLine1 END) AS physicalAddress1,
    MAX(CASE WHEN AddrTypeCD = 1 THEN AddressLine2 END) AS physicalAddress2,
    MAX(CASE WHEN AddrTypeCD = 1 THEN AddressLine3 END) AS physicalSuburb,
    MAX(CASE WHEN AddrTypeCD = 1 THEN AddressCity END)  AS physicalTown,
    MAX(CASE WHEN AddrTypeCD = 1 THEN AddressPostCode END) AS physicalPostalCode,

    MAX(CASE WHEN AddrTypeCD = 2 THEN AddressLine1 END) AS postalAddress1,
    MAX(CASE WHEN AddrTypeCD = 2 THEN AddressLine2 END) AS postalAddress2,
    MAX(CASE WHEN AddrTypeCD = 2 THEN AddressLine3 END) AS postalSuburb,
    MAX(CASE WHEN AddrTypeCD = 2 THEN AddressCity END)  AS postalTown,
    MAX(CASE WHEN AddrTypeCD = 2 THEN AddressPostCode END) AS postalCode
INTO #EntityAddress
FROM Customer.cust.EntityAddress
GROUP BY EntityNo;

-- Final SELECT Using Only Temp Tables
SELECT
      p.Policy_NO                     AS policy_NO
    , e.Entity_NO                     AS entityNo
    , p.Legacy_Pol_No                 AS legacy_Pol_No
    , b.Date_of_Commencement          AS dateOfCommencement
    , mph.Eff_From                    AS captureDate
    , pt.Telecom                      AS preferedCommunicationMethod
    , ct.S_Desc                       AS title 
    , ce.EntityTitleCD                AS titleID
    , ce.EntityName                   AS firstname
    , ce.EntitySurname                AS surname
    , ce.LegalRefNo                   AS legalRefNo
    , ce.LegalRefNoTypeCD             AS legalNumberType
    , ce.EntityDOB                    AS dateOfBirth
    , pt.TelTypeCD                    AS preferredTelTypeCd

    , CASE WHEN pt.TelTypeCD = 1 THEN pt.Telecom END AS emailAddress
    , pt.Telecom                     AS cellNumber 

    , ea.physicalAddress1            AS physicalAddress1
    , ea.physicalAddress2            AS physicalAddress2
    , ea.physicalSuburb              AS physicalSuburb
    , ea.physicalTown                AS physicalTown
    , ea.physicalPostalCode          AS physicalPostalCode

    , ea.postalAddress1              AS postalAddress1
    , ea.postalAddress2              AS postalAddress2
    , ea.postalSuburb                AS postalSuburb
    , ea.postalTown                  AS postalTown
    , ea.postalCode                  AS postalPostalCode

    , ce.EntityGenderCD              AS genderCD
    , cs.Smoker_CD                   AS smokerCd
    , cs.S_Desc                      AS smokerDescr

    , lpt.EffDate                    AS lastBillingDate
    , lpt.EffDate                    AS lastPaidDate
    , DATEADD(MONTH, 1, lpt.EffDate) AS nextBillingDate

    , CASE WHEN mp.Premium_Type_CD = 2 AND mp.Eff_To = '9999-12-31'
           THEN mp.Premium_Amt END   AS policyPremiumAmount

    , p.PremiumCount                 AS premiumCount
    , mp.Premium_Freq_CD             AS paymentFrequency

FROM polmas.M_Benefit b
JOIN polmas.M_Entity e                      ON b.Policy_NO = e.Policy_NO AND b.Benefit_ID = e.Benefit_ID
JOIN polmas.M_Policy p                      ON p.Policy_NO = e.Policy_NO
JOIN config.C_Role c                        ON e.Role_CD = c.Role_CD
JOIN config.C_Benefit_Type bt               ON b.Benefit_Type_CD = bt.Benefit_Type_CD
JOIN polmas.M_Policy_History mph            ON e.Policy_NO = mph.Policy_NO

JOIN Customer.cust.Entity ce                ON e.Entity_NO = ce.EntityNo
JOIN Customer.config.Title ct               ON ce.EntityTitleCD = ct.Title_CD

JOIN #PreferredTelecom pt                   ON e.Entity_NO = pt.EntityNo
JOIN #EntityAddress ea                      ON ce.EntityNo = ea.EntityNo
JOIN #LatestPolicyTransaction lpt           ON b.Policy_NO = lpt.PolicyNo

JOIN polmas.M_Attribute ma                  ON ma.Policy_NO = b.Policy_NO AND ma.Attribute_CD = 9
JOIN config.C_Smoker cs                     ON ma.Att_Amount = cs.Smoker_CD

JOIN polmas.M_Premium mp                    ON mp.Policy_NO = b.Policy_NO

WHERE p.Legacy_Pol_No = '612329975'
ORDER BY p.Policy_NO DESC, lpt.EffDate DESC;